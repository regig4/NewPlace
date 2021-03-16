using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentService.ApplicationCore.Domain.Entities
{
    public record DonationResult(Guid PaymentId, Guid UserId, ulong Amount, string Currency);
}
