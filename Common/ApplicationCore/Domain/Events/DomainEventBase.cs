using System;

namespace Common.ApplicationCore.Domain.Events
{
    public record DomainEventBase : IDomainEvent
    {
        public Guid Id { get; init; }

        public DateTime OccurredOn { get; }

        public bool Commited { get; set; }

        public DomainEventBase()
        {
            Id = Guid.NewGuid();
            OccurredOn = DateTime.UtcNow;
        }
    }
}
