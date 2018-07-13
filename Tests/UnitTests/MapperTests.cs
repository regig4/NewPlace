using AutoMapper;
using Xunit;

public class MapperTests
{
    [Fact]
    public void TestMap() 
    {
        // Arrange
        Mapper.Initialize(cfg =>
        {
            cfg.CreateMap<ApplicationCore.Models.Advertisement, ApplicationCore.DTOs.AdvertisementDetailsDto>()
                .ForMember(dest => dest.PricingType, opts => opts.MapFrom(src => src.Category.PricingType))
                .ForMember(dest => dest.ApartmentType, opts => opts.MapFrom(src => src.Category.ApartmentType))
                .ReverseMap();
        });

        var domain = new ApplicationCore.Models.Advertisement 
        {
            Category = new ApplicationCore.Models.Category() { Id = 1},
            Title = "AAA"
        };

        var dto = new ApplicationCore.DTOs.AdvertisementDetailsDto(domain);

        // Act
        var mapped = Mapper.Map<ApplicationCore.Models.Advertisement>(dto);

        // Assert
        Assert.Equal(mapped.Title, domain.Title);
        Assert.Equal(mapped.Category.PricingType, domain.Category.PricingType);
        Assert.Equal(mapped.Category.ApartmentType, domain.Category.ApartmentType);
    }
}