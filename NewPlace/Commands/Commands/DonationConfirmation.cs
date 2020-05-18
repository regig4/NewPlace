using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Models.Commands
{
    public class DonationConfirmation
    {
        public DonationConfirmation(int userId, decimal amount, string currency, DonationStatus status)
        {
            UserId = userId;
            Amount = amount;
            Currency = currency;
            Status = status;
        }

        public int UserId { get; }
        public decimal Amount { get; }
        public string Currency { get; }

        public enum DonationStatus { Success, InsufficientFunds, AccountNotFound, Failed}

        public DonationStatus Status { get; }
    }
}
