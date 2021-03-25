using Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogService.Application.Queries
{
    public record TopDonationsQuery(int Count) : IQuery<IReadOnlyList<Common.Dto.PaymentDto>>;
}

