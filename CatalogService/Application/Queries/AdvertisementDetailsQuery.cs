using System.Collections.Generic;
using Common.DTOs.Advertisement;

namespace CatalogService.Application.Queries
{
    public record struct AdvertisementDetailsQuery(uint Id) : IQuery<AdvertisementDetailsDto>;
}
