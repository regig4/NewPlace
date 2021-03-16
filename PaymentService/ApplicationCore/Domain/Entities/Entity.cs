using PaymentService.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentService.ApplicationCore.Domain.Entities
{
    public abstract class Entity
    {
        public Guid Id { get; set; }

        private List<IDomainEvent> _domainEvents;

        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents?.AsReadOnly();

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }

        protected void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents ??= new List<IDomainEvent>();

            this._domainEvents.Add(domainEvent);
        }

        protected void Apply(IDomainEvent domainEvent)
        {
            dynamic entity = this;
            Type eventType = domainEvent.GetType();

            foreach(var eventProperty in eventType.GetProperties())
            {
                var 
                eventProperty.SetValue(entity, eventProperty.GetValue(domainEvent));
            }
        }
    }
}
