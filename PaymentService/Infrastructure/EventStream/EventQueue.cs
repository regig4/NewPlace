using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Common.ApplicationCore.Domain.Entities;
using Common.ApplicationCore.Domain.Events;
using PaymentService.ApplicationCore.Application.Services;
using RabbitMQ.Client;

namespace PaymentService.Infrastructure.EventStream
{
    public class EventQueue : IEventQueue
    {
        public void Publish(Entity entity, List<IDomainEvent> uncommitedEvents)
        {
            ConnectionFactory factory = new ConnectionFactory() { HostName = "localhost" };
            using IConnection connection = factory.CreateConnection();
            using IModel channel = connection.CreateModel();
            channel.QueueDeclare(queue: "eventqueue",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);



            foreach (IDomainEvent e in uncommitedEvents)
            {
                string json = JsonSerializer.Serialize(
                new EventMessage
                {
                    EntityId = entity.Id,
                    EntityFullType = entity.GetType().AssemblyQualifiedName,
                    EntityAssembly = entity.GetType().Assembly.FullName,
                    EntityType = entity.GetType().FullName,
                    EventType = e.GetType().AssemblyQualifiedName,
                    Event = e
                });

                byte[] body = Encoding.UTF8.GetBytes(json);

                channel.BasicPublish(exchange: "",
                                 routingKey: "eventqueue",
                                 basicProperties: null,
                                 body: body);
            }
        }
    }
}
