using PaymentService.Domain.Enums;
using PaymentService.Domain.Events;
using PaymentService.Domain.Exceptions;
using PaymentService.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentService.Domain.Entities
{
    public class Payment : Entity
    {
        public Guid Id { get; set; }
        public User Payer { get; private set; }
        public User Payee { get; private set; }
        public TransactionType TransactionType { get; private set; }
        public PaymentStatus PaymentStatus { get; private set; }
        public Points Points { get; private set; }
        public decimal Amount { get; private set; }
        public string Currency { get; private set; }

        public static Payment CreateBonusForCreatingAccount(User user)
        {
            var payment = new Payment { Payee = user, PaymentStatus = PaymentStatus.Started };

            var paymentForCreatingAccountStarted = new PaymentForCreatingAccountStarted(
                payment.Id,
                payment.Payee,
                payment.Points
                );

            payment.AddDomainEvent(paymentForCreatingAccountStarted);

            return payment;
        }

        public void FinalizeBonusForCreatingAccount()
        {
            if (PaymentStatus != PaymentStatus.Started)
                throw new PaymentNotStartedException();
            if (Payee == null)
                throw new PayeeNotSetException();

            PaymentStatus = PaymentStatus.Completed;

            var paymentForCreatingAccountStarted = new PaymentForCreatingAccountCompleted(
                Id,
                Payee,
                Points
                );

            AddDomainEvent(paymentForCreatingAccountStarted);
        }

        public static Payment CreateDonation(ulong userId, ulong value, string currency)
        {
            var payment = new Payment
            {
                PaymentStatus = PaymentStatus.Started,
                TransactionType = TransactionType.Donation,
                Payer = new User { Id = userId },
                Amount = value,
                Currency = currency
            };

            return payment;
        }
    }
}
