using System;

namespace Common.ApplicationCore.Domain.Events
{
    public interface IDomainEvent
    {
        Guid Id { get; }

        DateTime OccurredOn { get; }
        bool Commited { get; set; }
    }
}
