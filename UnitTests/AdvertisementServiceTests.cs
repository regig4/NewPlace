using ApplicationCore.Services;
using Infrastructure.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using UnitTests.Mocks;

namespace UnitTests
{
    [TestClass]
    public class AdvertisementServiceTests
    {
        [TestMethod]
        public void GetByIdTest()
        {
            // Arrange
            IAdvertisementService service = new AdvertisementService(new AdvertisementRepositoryMock());

            // Act
            var advertisement = service.GetById(-1); 

            // Assert
            Assert.AreEqual(advertisement.Id, -1);
            Assert.AreEqual(advertisement.Apartment.Utilities.First().Cost, 200);
            Assert.AreEqual(advertisement.Apartment.Id, -1);
            Assert.AreEqual(advertisement.Apartment.Category.Id, -1);

        }
    }
}
