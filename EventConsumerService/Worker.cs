using EventConsumerService.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PaymentService.Domain.Events;
using PaymentService.Infrastructure.EventStream;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace EventConsumerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        static SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1, 1);

        public Worker(ILogger<Worker> logger, IConfiguration configuration)
        {
            _logger = logger;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var uri = Configuration.GetServiceUri(name: "rabbit", binding: "default") ?? new Uri("amqp://localhost:5672");

            var factory = new ConnectionFactory()
            {
                Endpoint = new AmqpTcpEndpoint(uri),
                UserName = "abc",
                Password = "123"
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "eventqueue",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var consumer = new EventingBasicConsumer(channel);
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
                var body = ea.Body.ToArray();
                var json = Encoding.UTF8.GetString(body);
                _logger.LogInformation(json);
                var msg = JsonSerializer.Deserialize<EventMessage>(json);

                var eventJson = ((JsonElement)msg.Event).GetRawText();
                var eventToApply = (DomainEventBase)JsonSerializer.Deserialize(eventJson, Type.GetType(msg.EventType));

                dynamic repository = RepositoryByType.Instance[Type.GetType(msg.EntityFullType)];
                var entity = await repository.Get(msg.EntityId);

                bool isInsert = false;

                if (entity == null)
                {
                    isInsert = true;
                    entity = Activator.CreateInstance(msg.EntityAssembly, msg.EntityType).Unwrap();
                }

                entity.Apply(eventToApply);

                if (isInsert)
                    await repository.Add(entity);
                else
                    await repository.Update(entity);
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
