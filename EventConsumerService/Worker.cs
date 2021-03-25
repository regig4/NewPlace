using EventConsumerService.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PaymentService.Domain.Events;
using PaymentService.Infrastructure.EventStream;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace EventConsumerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private PaymentService.ApplicationCore.Domain.Entities.Payment _payment;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "eventqueue",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var json = Encoding.UTF8.GetString(body);
                _logger.LogInformation(json);
                var msg = JsonSerializer.Deserialize<EventMessage>(json);

                var eventJson = ((JsonElement)msg.Event).GetRawText();
                var eventToApply = (DomainEventBase)JsonSerializer.Deserialize(eventJson, Type.GetType(msg.EventType));

                var repository = RepositoryByType.Instance[Type.GetType(msg.EntityFullType)];
                _payment = await repository.Get(msg.EntityId);

                bool isInsert = false;

                if(_payment == null)
                {
                    isInsert = true;
                    _payment = (PaymentService.ApplicationCore.Domain.Entities.Payment)Activator.CreateInstance(msg.EntityAssembly, msg.EntityType).Unwrap();
                }

                _payment.Apply(eventToApply);

                if (isInsert)
                    try
                    {
                        await repository.Add(_payment);
                    }
                    catch (Exception)
                    {
                        _payment = await repository.Get(_payment.Id);
                        _payment.Apply(eventToApply);
                        await repository.Update(_payment);
                    }
                else
                    await repository.Update(_payment);
            };

            channel.BasicConsume(queue: "eventqueue",
                                 autoAck: true,
                                 consumer: consumer);

            while (!stoppingToken.IsCancellationRequested)
            {
            }
        }
    }
}
