using PaymentService.ApplicationCore.Domain.Entities;
using PaymentService.ApplicationCore.Domain.ValueObjects;
using System;

namespace PaymentService.Domain.Events
{
    public class PaymentForCreatingAccountStarted : DomainEventBase
    {
        public PaymentForCreatingAccountStarted(Guid id, User payee, PointsValue points)
        {
            Id = id;
            Payee = payee;
            Points = points;
        }

        public User Payee { get; }
        public PointsValue Points { get; }
    }
}
