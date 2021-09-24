using PaymentService.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentService.ApplicationCore.Domain.Entities
{
    public abstract class Entity : IEntity
    {
        public Guid Id { get; set; }

        private List<IDomainEvent> _domainEvents;

        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents?.AsReadOnly();

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }

        public void CommitAllEvents()
        {
            if (_domainEvents == null)
                return;

            foreach (var e in _domainEvents)
                e.Commited = true;
        }

        protected void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents ??= new List<IDomainEvent>();
            this._domainEvents.Add(domainEvent);
        }

        public void Apply(IDomainEvent domainEvent)
        {
            Type eventType = domainEvent.GetType();
            Type entityType = GetType();

            foreach(var eventProperty in eventType.GetProperties())
            {
                if (eventProperty.Name == nameof(Id) && (Guid)entityType.GetProperty(eventProperty.Name).GetValue(this) != Guid.Empty)
                    continue;

                var entityProperty = entityType.GetProperty(eventProperty.Name);

                if(entityProperty != null)
                    entityProperty.SetValue(this, eventProperty.GetValue(domainEvent));
            }
        }
    }
}
