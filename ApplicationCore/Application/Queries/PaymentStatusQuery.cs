using Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationCore.Application.Queries
{
    public record PaymentStatusQuery(Guid PaymentId) : IQuery<PaymentStatusResponse>;
}
