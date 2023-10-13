using System;
using Common.ApplicationCore.Domain.Events;
using PaymentService.ApplicationCore.Domain.Entities;
using PaymentService.ApplicationCore.Domain.ValueObjects;

namespace PaymentService.Domain.Events
{
    public record PaymentForCreatingAccountCompleted : DomainEventBase
    {
        public PaymentForCreatingAccountCompleted(Guid id, User payee, PointsValue points)
        {
            Id = id;
            Payee = payee;
            Points = points;
        }

        public User Payee { get; }
        public PointsValue Points { get; }
    }
}
