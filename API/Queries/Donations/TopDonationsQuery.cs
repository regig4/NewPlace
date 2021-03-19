using Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Queries.Donations
{
    public record TopDonationsQuery(int Count = 10) : IQuery<IReadOnlyList<PaymentDto>>;
}
