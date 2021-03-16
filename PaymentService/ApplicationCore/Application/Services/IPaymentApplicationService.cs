using PaymentService.ApplicationCore.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace PaymentService.ApplicationCore.Application.Services
{
    public interface IPaymentApplicationService
    {
        Task<DonationResult> Donate(Guid userId, ulong amount, string currency);
    }
}
