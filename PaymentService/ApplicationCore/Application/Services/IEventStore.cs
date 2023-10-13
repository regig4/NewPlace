using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.ApplicationCore.Domain.Events;

namespace PaymentService.ApplicationCore.Application.Services
{
    public interface IEventStore
    {
        Task<List<IDomainEvent>> GetEvents(Guid entityId);
        Task SaveEvents(Guid id, List<IDomainEvent> uncommitedEvents);
    }
}
