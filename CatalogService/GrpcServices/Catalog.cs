using AutoMapper;
using CatalogService.Application.Queries;
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
        private readonly IMapper _mapper;

        public CatalogService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public override async Task<AdvertisementDetailsResult> AdvertisementDetails(AdvertisementDetailsRequest request, ServerCallContext context)
        {
            var advertisement = await _mediator.Send(new AdvertisementDetailsQuery(request.Id));
            return _mapper.Map<AdvertisementDetailsResult>(advertisement);
        }

        public async override Task<TopDonationResult> TopDonations(TopDonationsRequest request, ServerCallContext context)
        {
            try
            {
                var donations = (IReadOnlyCollection<Common.Dto.PaymentDto>)await _mediator.Send(new TopDonationsQuery((int)request.Count));
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
