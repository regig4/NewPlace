using Common.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Models.Commands
{
    public class DonateCommand : ICommand<DonationConfirmation>
    {
        public DonateCommand(Guid userId, ulong amount, string currency)
        {
            UserId = userId;
            Amount = amount;
            Currency = currency;
        }

        public Guid UserId { get; }
        public ulong Amount { get; }
        public string Currency { get; }
    }
}
