using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CatalogService.Application.Queries;
using Dapper;
using PaymentDto = Common.Dto.PaymentDto;
using TopDonationsQuery = CatalogService.Application.Queries.TopDonationsQuery;

namespace API.Queries.Donations;

public class TopDonationsQueryHandler : IQueryHandler<TopDonationsQuery, IReadOnlyList<PaymentDto>>
{
    public async Task<IReadOnlyList<PaymentDto>> Handle(TopDonationsQuery request, CancellationToken cancellationToken)
    {
        using SqlConnection connection = new SqlConnection(Infrastructure.Configuration.Configuration.DefaultConnectionString);
        IEnumerable<PaymentDto> result = await connection.QueryAsync<PaymentDto>(@$"select top {request.Count} payer_id as {nameof(PaymentDto.UserId)},
                                                                    money_amount as {nameof(PaymentDto.Amount)},
                                                                    money_currency as {nameof(PaymentDto.Currency)}
                                                                from payment.Payments order by amount desc;");
        return result.ToList().AsReadOnly();
    }
}
