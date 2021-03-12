using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Models.Commands
{
    public class DonateCommand : ICommand<DonationConfirmation>
    {
        public DonateCommand(ulong userId, ulong amount, string currency)
        {
            UserId = userId;
            Amount = amount;
            Currency = currency;
        }

        public ulong UserId { get; }
        public ulong Amount { get; }
        public string Currency { get; }
    }
}
