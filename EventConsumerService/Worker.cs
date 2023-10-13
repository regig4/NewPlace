using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Common.ApplicationCore.Domain.Events;
using EventConsumerService.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PaymentService.Infrastructure.EventStream;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace EventConsumerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private static readonly SemaphoreSlim _semaphoreSlim = new(initialCount: 1, maxCount: 1);

        public Worker(ILogger<Worker> logger, IConfiguration configuration)
        {
            _logger = logger;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Uri uri = Configuration.GetServiceUri(name: "rabbit", binding: "default") ?? new Uri("amqp://localhost:5672");

            ConnectionFactory factory = new ConnectionFactory()
            {
                Endpoint = new AmqpTcpEndpoint(uri),
                UserName = "abc",
                Password = "123"
            };

            using IConnection connection = factory.CreateConnection();
            using IModel channel = connection.CreateModel();

            channel.QueueDeclare(queue: "eventqueue",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
            consumer.Received += HandleEvent;

            channel.BasicConsume(queue: "eventqueue",
                                 autoAck: true,
                                 consumer: consumer);

            while (!stoppingToken.IsCancellationRequested)
            {
            }
        }

        private async void HandleEvent(object? model, BasicDeliverEventArgs ea)
        {
            try
            {
                await _semaphoreSlim.WaitAsync();
                byte[] body = ea.Body.ToArray();
                string json = Encoding.UTF8.GetString(body);
                _logger.LogInformation(json);
                EventMessage msg = JsonSerializer.Deserialize<EventMessage>(json);

                string eventJson = ((JsonElement)msg.Event).GetRawText();
                DomainEventBase eventToApply = (DomainEventBase)JsonSerializer.Deserialize(eventJson, Type.GetType(msg.EventType));

                dynamic repository = RepositoryByType.Instance[Type.GetType(msg.EntityFullType)];
                dynamic entity = await repository.Get(msg.EntityId);

                bool isInsert = false;

                if (entity == null)
                {
                    isInsert = true;
                    entity = Activator.CreateInstance(msg.EntityAssembly, msg.EntityType).Unwrap();
                }

                entity.Apply(eventToApply);

                if (isInsert)
                {
                    await repository.Add(entity);
                }
                else
                {
                    await repository.Update(entity);
                }
            }
            catch (Exception)
            {
                //TODO: logging exceptions 
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }
    }
}
