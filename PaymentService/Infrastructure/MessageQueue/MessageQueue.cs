using System.Text;
using Common.IntegrationEvents.Payment;
using RabbitMQ.Client;

namespace PaymentService.Infrastructure.MessageQueue
{
    public class MessageQueue : IMessageQueue
    {
        public async void Publish(DonationSuccessfulEvent @event)
        {
            ConnectionFactory factory = new ConnectionFactory() { HostName = "localhost" };
            using IConnection connection = factory.CreateConnection();
            using IModel channel = connection.CreateModel();
            channel.QueueDeclare(queue: "queue",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            byte[] body = Encoding.UTF8.GetBytes(@event.Id.ToString());

            channel.BasicPublish(exchange: "",
                                 routingKey: "queue",
                                 basicProperties: null,
                                 body: body);
        }
    }
}
