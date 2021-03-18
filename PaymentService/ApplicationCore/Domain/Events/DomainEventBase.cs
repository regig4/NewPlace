using System;

namespace PaymentService.Domain.Events
{
    public record DomainEventBase : IDomainEvent
    {
        public Guid Id { get; init; }

        public DateTime OccurredOn { get; }

        public bool Commited { get; set; }

        public DomainEventBase()
        {
            this.Id = Guid.NewGuid();
            this.OccurredOn = DateTime.UtcNow;
        }
    }
}
