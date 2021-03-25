using Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationCore.Application.Queries
{
    public record TopDonationsQuery(uint Count) : IQuery<List<PaymentDto>>;
}
