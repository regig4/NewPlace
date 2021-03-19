using Common.Dto;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace API.Queries.Donations
{
    public class TopDonationsQueryHandler : IQueryHandler<TopDonationsQuery, IReadOnlyList<PaymentDto>>
    {
        public async Task<IReadOnlyList<PaymentDto>> Handle(TopDonationsQuery request, CancellationToken cancellationToken)
        {
            using var connection = new SqlConnection(Infrastructure.Configuration.Configuration.DefaultConnectionString);
            var result = await connection.QueryAsync<PaymentDto>($"select top {request.Count} * from Payments order by amount desc;");
            return result.ToList().AsReadOnly();
        }
    }
}
