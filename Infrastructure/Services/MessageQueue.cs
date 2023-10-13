using System;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Services;
using Common.IntegrationEvents;
using Common.IntegrationEvents.Payment;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Infrastructure.Services
{
    public class MessageQueue : IMessageQueue
    {
        public async Task<IntegrationEvent> WaitForEvent(IntegrationEvent @event, TimeSpan? timeSpan = null)
        {
            if (timeSpan == null)
            {
                timeSpan = TimeSpan.FromSeconds(5);
            }

            ConnectionFactory? factory = new ConnectionFactory() { HostName = "localhost" };
            using IConnection? connection = factory.CreateConnection();
            using IModel? channel = connection.CreateModel();

            channel.QueueDeclare(queue: "queue",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            EventingBasicConsumer? consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                byte[]? body = ea.Body.ToArray();
                string? message = Encoding.UTF8.GetString(body);
                @event = CreateReply(@event, message);
            };
            channel.BasicConsume(queue: "queue",
                                 autoAck: true,
                                 consumer: consumer);

            await Task.Delay(timeSpan.Value);

            return @event;
        }

        private IntegrationEvent CreateReply(IntegrationEvent @event, string message)
        {
            return @event switch
            {
                DonationSuccessfulEvent => new DonationSuccessfulEvent(Guid.Parse(message)),
                _ => throw new ArgumentException(message: "invalid integration event", paramName: nameof(@event))
            };
        }
    }
}
