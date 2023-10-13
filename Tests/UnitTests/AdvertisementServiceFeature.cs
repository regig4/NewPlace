using System.Linq;
using ApplicationCore.DTOs;
using FluentAssertions;
using Infrastructure.Services;
using UnitTests.Mocks;
using Xbehave;

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
