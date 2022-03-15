using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var advertisementBuilder = new AdvertisementBuilder();
            var advertisement = advertisementBuilder
                .WithEstateType(EstateType.House)
                .WithPaymentType(PricingType.Sale)
                .Build();

            Assert.Equal(EstateType.House, advertisement.Category.ApartmentType);
            Assert.Equal(PricingType.Sale, advertisement.Category.PricingType);
        }
    }
}
