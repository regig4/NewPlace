using PaymentService.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentService.ApplicationCore.Application.Services
{
    public interface IEventStore
    {
        Task<List<IDomainEvent>> GetEvents(Guid entityId);
        Task SaveEvents(Guid id, List<IDomainEvent> uncommitedEvents);
    }
}
