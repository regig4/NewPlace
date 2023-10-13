using System;
using Common.ApplicationCore.Domain.Events;
using PaymentService.Domain.Enums;

namespace PaymentService.ApplicationCore.Domain.Events
{
    public record DonationCompleted : DomainEventBase
    {
        public DonationCompleted(Guid id)
        {
            Id = id;
            PaymentStatus = PaymentStatus.Completed;
        }

        public Guid Id { get; }
        public PaymentStatus PaymentStatus { get; }
    }
}
