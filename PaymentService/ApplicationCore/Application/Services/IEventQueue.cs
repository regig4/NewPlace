using System.Collections.Generic;
using Common.ApplicationCore.Domain.Entities;
using Common.ApplicationCore.Domain.Events;

namespace PaymentService.ApplicationCore.Application.Services
{
    public interface IEventQueue
    {
        void Publish(Entity entity, List<IDomainEvent> uncommitedEvents);
    }
}
