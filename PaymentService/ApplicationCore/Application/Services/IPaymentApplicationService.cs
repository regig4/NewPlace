﻿using System;
using System.Threading.Tasks;
using PaymentService.ApplicationCore.Domain.Entities;

namespace PaymentService.ApplicationCore.Application.Services
{
    public interface IPaymentApplicationService
    {
        Task<DonationResult> Donate(Guid userId, ulong amount, string currency);
    }
}
