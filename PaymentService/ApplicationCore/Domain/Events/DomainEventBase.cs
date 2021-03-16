using System;

namespace PaymentService.Domain.Events
{
    public class DomainEventBase : IDomainEvent
    {
        public Guid Id { get; init; }

        public DateTime OccurredOn { get; }

        public DomainEventBase()
        {
            this.Id = Guid.NewGuid();
            this.OccurredOn = DateTime.UtcNow;
        }
    }
}
