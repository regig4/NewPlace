using System.Threading;
using System.Threading.Tasks;
using Common.DTOs.Advertisement;

namespace CatalogService.Application.Queries
{
    public class AdvertisementDetailsQueryHandler : IQueryHandler<AdvertisementDetailsQuery, AdvertisementDetailsDto>
    {
        public Task<AdvertisementDetailsDto> Handle(AdvertisementDetailsQuery request, CancellationToken cancellationToken)
        {
            throw new System.Exception();
        }
    }
}
