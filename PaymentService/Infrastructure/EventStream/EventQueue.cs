using PaymentService.ApplicationCore.Application.Services;
using PaymentService.ApplicationCore.Domain.Entities;
using PaymentService.Domain.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PaymentService.Infrastructure.EventStream
{
    public class EventQueue : IEventQueue
    {
        public void Publish(Entity entity, List<IDomainEvent> uncommitedEvents)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queue: "eventqueue",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);



            foreach (var e in uncommitedEvents)
            {
                var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(
                new EventMessage
                {
                    EntityId = entity.Id,
                    EntityType = entity.GetType().FullName,
                    EventType = e.GetType().AssemblyQualifiedName,
                    Event = e
                }));

                channel.BasicPublish(exchange: "",
                                 routingKey: "eventqueue",
                                 basicProperties: null,
                                 body: body);
            }
        }
    }
}
