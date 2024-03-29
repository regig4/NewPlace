﻿using System;
using Common.Dto;

namespace ApplicationCore.Application.Commands
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
