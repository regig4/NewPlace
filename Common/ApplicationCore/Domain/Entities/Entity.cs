using System;
using System.Collections.Generic;
using Common.ApplicationCore.Domain.Events;

namespace Common.ApplicationCore.Domain.Entities
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
            {
                return;
            }

            foreach (IDomainEvent e in _domainEvents)
            {
                e.Commited = true;
            }
        }

        protected void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents ??= new List<IDomainEvent>();
            _domainEvents.Add(domainEvent);
        }

        public void Apply(IDomainEvent domainEvent)
        {
            Type eventType = domainEvent.GetType();
            Type entityType = GetType();

            foreach (System.Reflection.PropertyInfo eventProperty in eventType.GetProperties())
            {
                if (eventProperty.Name == nameof(Id) && (Guid)entityType.GetProperty(eventProperty.Name).GetValue(this) != Guid.Empty)
                {
                    continue;
                }

                System.Reflection.PropertyInfo entityProperty = entityType.GetProperty(eventProperty.Name);

                if (entityProperty != null)
                {
                    entityProperty.SetValue(this, eventProperty.GetValue(domainEvent));
                }
            }
        }
    }
}
