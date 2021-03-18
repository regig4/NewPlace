using PaymentService.ApplicationCore.Domain.Entities;
using PaymentService.ApplicationCore.Domain.ValueObjects;
using PaymentService.Domain.Enums;
using PaymentService.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentService.ApplicationCore.Domain.Events
{
    public record DonationCreated : DomainEventBase 
    {
        public DonationCreated(User payer, MoneyValue moneyValue)
        {
            Payer = payer;
            MoneyValue = moneyValue;
            PaymentStatus = PaymentStatus.Started;
            TransactionType = TransactionType.Donation;
        }

        public PaymentStatus PaymentStatus { get; }
        public TransactionType TransactionType { get; }
        public User Payer { get; }
        public MoneyValue MoneyValue { get; }
    };
}
