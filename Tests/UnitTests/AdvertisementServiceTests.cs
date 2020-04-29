using ApplicationCore.Services;
using Infrastructure.Services;
using System.Linq;
using UnitTests.Mocks;
using Xunit;
using Xbehave;
using FluentAssertions;
using ApplicationCore.DTOs;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using Infrastructure.Repositories;

namespace UnitTests
{
    public class AdvertisementServiceFeature
    {
        [Scenario]
        public void GetByIdTest(IAdvertisementService service, AdvertisementDetailsDto dto)
        {
            "Given AdvertisementService".x(() => 
                service = new AdvertisementService(new AdvertisementRepositoryStub(), new ImageService()));
            
            "When getting advertisement by id".x(() => dto = service.GetById(-1)); 

            "Then we get the id of advertisement"
                .x(() => dto.Id.Should().Be(-1));

            "And we get cost of utilities of advertisement"
                .x(() => dto.Utilities.First().cost.Should().Be(200));
        }

        [Scenario]
        public void CheckIfAsync(IAdvertisementService service, Task<IEnumerable<AdvertisementDto>> task)
        {
            "Given AdvertisementService".x(() =>
                service = new AdvertisementService(new AdvertisementRepository(), new ImageService()));

            "When we try to get items asynchronously".x(() => task = service.GetAllAsync());

            "Then task result is not immediately available".x(() => task.IsCompleted.Should().Be(false));

            "And when we wait for completion we get list of advertisements".x(() => task.Result.Should().NotBeEmpty());
        }
    }
}
