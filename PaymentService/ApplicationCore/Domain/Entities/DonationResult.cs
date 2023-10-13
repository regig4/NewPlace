using System;

namespace PaymentService.ApplicationCore.Domain.Entities
{
    public record DonationResult(Guid PaymentId, Guid UserId, ulong Amount, string Currency);
}
