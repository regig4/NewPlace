using AutoMapper;

namespace CatalogService.Infrastructure.Configuration
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Common.DTOs.Advertisement.AdvertisementDetailsDto, AdvertisementDetailsResult>();
        }
    }
}
