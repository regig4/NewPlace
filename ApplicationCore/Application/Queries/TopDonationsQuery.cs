using System.Collections.Generic;
using Common.Dto;

namespace ApplicationCore.Application.Queries
{
    public record TopDonationsQuery(uint Count) : IQuery<List<PaymentDto>>;
}
