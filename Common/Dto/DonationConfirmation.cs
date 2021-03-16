using System;

namespace Common.Dto
{
    public record DonationConfirmation
    {
        public DonationConfirmation() { }

        public DonationConfirmation(Guid paymentId, Guid userId, decimal amount, string currency, DonationStatus status)
        {
            PaymentId = paymentId;
            UserId = userId;
            Amount = amount;
            Currency = currency;
            Status = status;
        }

        public Guid PaymentId { get; init; }
        public Guid UserId { get; init; }
        public decimal Amount { get; init; }
        public string Currency { get; init; }

        public enum DonationStatus { Success, InsufficientFunds, AccountNotFound, Failed}

        public DonationStatus Status { get; init; }
    }
}
