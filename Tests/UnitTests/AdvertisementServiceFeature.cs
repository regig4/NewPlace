using ApplicationCore.Services;
using Infrastructure.Services;
using System.Linq;
using UnitTests.Mocks;
using Xbehave;
using FluentAssertions;
using ApplicationCore.DTOs;
using AdvertisementService.ApplicationCore.Application.Services;

namespace Tests.UnitTests
{
    public class AdvertisementServiceFeature
    {
        [Scenario]
        public void GetByIdTest(AdvertisementService.ApplicationCore.Application.Services.IAdvertisementApplicationService service, AdvertisementDetailsDto dto)
        {
            "Given AdvertisementService".x(() => 
                service = new AdvertisementApplicationService(new AdvertisementRepositoryStub(), new ImageService()));
            
            "When getting advertisement by id".x(() => dto = service.GetById(-1)); 

            "Then we get the id of advertisement"
                .x(() => dto.Id.Should().Be(-1));

            "And we get cost of utilities of advertisement"
                .x(() => dto.Utilities.First().cost.Should().Be(200));
        }
    }
}
