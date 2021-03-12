using Common.IntegrationEvents.Payment;
using RabbitMQ.Client;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PaymentService.Infrastructure.MessageQueue
{
    public class MessageQueue : IMessageQueue
    {
        public async void Publish(DonationSuccessfulEvent @event)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            channel.QueueDeclare(queue: "queue",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var body = Encoding.UTF8.GetBytes(@event.Id.ToString());
            
            channel.BasicPublish(exchange: "",
                                 routingKey: "queue",
                                 basicProperties: null,
                                 body: body);
        }
    }
}
