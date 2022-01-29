using Common.ApplicationCore.Domain.Entities;
using PaymentService.ApplicationCore.Domain.Events;
using PaymentService.ApplicationCore.Domain.ValueObjects;
using PaymentService.Domain.Enums;
using PaymentService.Domain.Events;
using PaymentService.Domain.Exceptions;
using System;

namespace PaymentService.ApplicationCore.Domain.Entities
{
    public class Payment : Entity
    {
        public User Payer { get; private set; }
        public User Payee { get; private set; }
        public TransactionType TransactionType { get; private set; }
        public PaymentStatus PaymentStatus { get; private set; }
        public PointsValue PointsValue { get; private set; }
        public MoneyValue MoneyValue { get; private set; }

        public static Payment CreateBonusForCreatingAccount(User user)
        {
            var payment = new Payment { Payee = user, PaymentStatus = PaymentStatus.Started };

            var paymentForCreatingAccountStarted = new PaymentForCreatingAccountStarted(
                payment.Id,
                payment.Payee,
                payment.PointsValue
                );

            payment.AddDomainEvent(paymentForCreatingAccountStarted);

            return payment;
        }

        public void CompleteBonusForCreatingAccount()
        {
            if (PaymentStatus != PaymentStatus.Started)
                throw new PaymentNotStartedException();
            if (Payee == null)
                throw new PayeeNotSetException();

            PaymentStatus = PaymentStatus.Completed;

            var paymentForCreatingAccountStarted = new PaymentForCreatingAccountCompleted(
                Id,
                Payee,
                PointsValue
                );

            AddDomainEvent(paymentForCreatingAccountStarted);
        }

        public static Payment CreateDonation(Guid userId, ulong value, string currency)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException(nameof(userId));
            if (value <= 0)
                throw new ArgumentException(nameof(value));
            if (string.IsNullOrWhiteSpace(currency))
                throw new ArgumentException(nameof(currency));

            var payment = new Payment();

            var donationCreated = new DonationCreated(new User { Id = userId }, new MoneyValue(value, currency));
            payment.Apply(donationCreated);
            payment.AddDomainEvent(donationCreated);

            return payment;
        }

        public void CompleteDonation(Guid id)
        {
            if (PaymentStatus != PaymentStatus.Started)
                throw new PaymentNotStartedException();

            var donationCompleted = new DonationCompleted(id);
            Apply(donationCompleted);
            AddDomainEvent(donationCompleted);
        }
    }
}
