using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CatalogService.Application.Queries;
using Grpc.Core;
using MediatR;

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
            Common.DTOs.Advertisement.AdvertisementDetailsDto advertisement = await _mediator.Send(new AdvertisementDetailsQuery(request.Id));
            return _mapper.Map<AdvertisementDetailsResult>(advertisement);
        }

        public override async Task<TopDonationResult> TopDonations(TopDonationsRequest request, ServerCallContext context)
        {
            try
            {
                IReadOnlyCollection<Common.Dto.PaymentDto> donations = await _mediator.Send(new TopDonationsQuery((int)request.Count));
                TopDonationResult result = new TopDonationResult();
                foreach (Common.Dto.PaymentDto d in donations)
                {
                    result.Payments.Add(new PaymentDto
                    {
                        Amount = d.Amount.ToString(),
                        Currency = d.Currency,
                        UserId = d.UserId.ToString()
                    });
                }

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
