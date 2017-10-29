using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using ApplicationCore.Models;

namespace UnitTests.Mocks
{
    class AdvertisementRepositoryMock : IAdvertisementRepository
    {
        public Advertisement Find(Func<Advertisement, bool> condition)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Advertisement> FindAll(Func<Advertisement, bool> condition, int quantity = int.MaxValue)
        {
            throw new NotImplementedException();
        }

        public Advertisement GetById(int id)
        {
            return new Advertisement()
            {
                Apartment = new Apartment()
                {
                    Area = 56,
                    Id = -1,
                    Utilities = new List<Utility>() { new Utility() { Id = -1, Name = "TestUtility", Cost = 200 } }
                },
                Id = -1,
                Price = 2000,
                Provision = 2000,
                User = new User()
                {
                    Id = -1,
                    Agency = new Agency() { Id = -1, Name = "TestAgency", Address = "TestAddress", Information = "TestInformation" }
                },
                Category = new Category() { Id = -1, ApartmentType = ApartmentType.Flat }
            };
        }
    }
}
