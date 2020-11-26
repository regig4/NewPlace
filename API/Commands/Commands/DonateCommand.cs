using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Models.Commands
{
    public class DonateCommand : ICommand<DonationConfirmation>
    {
        public DonateCommand(int userId, ulong amount, string currency)
        {
            UserId = userId;
            Amount = amount;
            Currency = currency;
        }

        public int UserId { get; }
        public ulong Amount { get; }
        public string Currency { get; }
    }
}
