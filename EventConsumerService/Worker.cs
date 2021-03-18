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
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var json = Encoding.UTF8.GetString(body);
                var msg = JsonSerializer.Deserialize<EventMessage>(json);
                // todo: get from db based from type and id
                if(_payment == null)
                    _payment = (PaymentService.ApplicationCore.Domain.Entities.Payment) Activator.CreateInstance("PaymentService", msg.EntityType).Unwrap();

                var eventJson = ((JsonElement)msg.Event).GetRawText();
                var eventToApply = (DomainEventBase) JsonSerializer.Deserialize(eventJson, Type.GetType(msg.EventType));
                _payment.Apply(eventToApply);
                // todo: save to db 
            };

            channel.BasicConsume(queue: "eventqueue",
                                 autoAck: true,
                                 consumer: consumer);

            while (!stoppingToken.IsCancellationRequested)
            {

                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
