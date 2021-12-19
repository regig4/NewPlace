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
            try
            {
                var donations = (IReadOnlyCollection<Common.Dto.PaymentDto>)await _mediator.Send(new Application.Queries.TopDonationsQuery((int)request.Count));
                var result = new TopDonationResult();
                foreach(var d in donations)
                    result.Payments.Add(new PaymentDto 
                    {
                        Amount = d.Amount.ToString(),
                        Currency = d.Currency,
                        UserId = d.UserId.ToString()
                    });
                return result;
            }
            catch (Exception)
            {

                throw;
            }        
        }
    }
}
