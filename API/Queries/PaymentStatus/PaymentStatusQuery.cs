using Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Queries.PaymentStatus
{
    public record PaymentStatusQuery(Guid PaymentId) : IQuery<PaymentStatusResponse>;
}
