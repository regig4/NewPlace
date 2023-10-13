using AdvertisementService.ApplicationCore.Domain.Builders;
using ApplicationCore.Models;
using Xunit;

namespace Tests.UnitTests
{
    public class AdvertisementTests
    {
        [Fact]
        public void AdvertisementBuilderTest()
        {
            AdvertisementBuilder advertisementBuilder = new AdvertisementBuilder();
            Advertisement advertisement = advertisementBuilder
                .WithEstateType(EstateType.House)
                .WithPaymentType(PricingType.Sale)
                .Build();

            Assert.Equal(EstateType.House, advertisement.Category.ApartmentType);
            Assert.Equal(PricingType.Sale, advertisement.Category.PricingType);
        }
    }
}
