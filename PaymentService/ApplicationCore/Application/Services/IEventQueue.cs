using PaymentService.ApplicationCore.Domain.Entities;
using PaymentService.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentService.ApplicationCore.Application.Services
{
    public interface IEventQueue
    {
        void Publish(Entity entity, List<IDomainEvent> uncommitedEvents);
    }
}
