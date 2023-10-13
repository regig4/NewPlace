using System.Collections.Generic;

namespace CatalogService.Application.Queries
{
    public record TopDonationsQuery(int Count) : IQuery<IReadOnlyList<Common.Dto.PaymentDto>>;
}

