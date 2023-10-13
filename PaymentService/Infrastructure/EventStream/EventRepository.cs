using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.ApplicationCore.Domain.Entities;
using Common.ApplicationCore.Domain.Events;
using PaymentService.ApplicationCore.Application.Repositories;
using PaymentService.ApplicationCore.Application.Services;

namespace PaymentService.Infrastructure.EventStream
{
    public class EventRepository : IEventRepository
    {
        private readonly IEventStore _store;
        private readonly IEventQueue _queue;

        public EventRepository(IEventStore store, IEventQueue queue)
        {
            _store = store;
            _queue = queue;
        }

        public async Task<List<IDomainEvent>> GetEvents(Guid entityId)
        {
            return await _store.GetEvents(entityId);
        }

        public async Task SaveEvents(Entity entity)
        {
            List<IDomainEvent> uncommitedEvents = entity.DomainEvents.Where(e => !e.Commited).ToList();
            await _store.SaveEvents(entity.Id, uncommitedEvents);
            _queue.Publish(entity, uncommitedEvents);
        }
    }
}
