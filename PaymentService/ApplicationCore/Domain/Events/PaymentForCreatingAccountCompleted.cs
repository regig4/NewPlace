using PaymentService.Domain.Entities;
using PaymentService.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentService.Domain.Events
{
    public class PaymentForCreatingAccountCompleted : DomainEventBase
    {
        public PaymentForCreatingAccountCompleted(Guid id, User payee, Points points)
        {
            Id = id;
            Payee = payee;
            Points = points;
        }

        public Guid Id { get; }
        public User Payee { get; }
        public Points Points { get; }
    }
}
