using Grpc.Core;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogService
{
    public class CatalogService : Catalog.CatalogBase
    {
        private readonly IMediator _mediator;

        public CatalogService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async override Task<TopDonationResult> TopDonations(TopDonationsQuery request, ServerCallContext context)
        {
            var donations = (List<PaymentDto>)await _mediator.Send(request);
            var result = new TopDonationResult();
            result.Payments.Add(donations);
            return result;
        }
    }
}
