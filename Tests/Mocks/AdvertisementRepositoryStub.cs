using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using ApplicationCore.Models;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Linq;

namespace UnitTests.Mocks
{
    class AdvertisementRepositoryStub : IAdvertisementRepository
    {
        public Advertisement Find(Func<Advertisement, bool> condition)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Advertisement> FindAll(Func<Advertisement, bool> condition, int quantity = int.MaxValue)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Advertisement>> FindAll(Expression<Func<Advertisement, bool>> condition, int quantity = int.MaxValue)
        {
            throw new NotImplementedException();
        }

        public Advertisement GetById(int id)
        {
            return new Advertisement()
            {
                Estate = new Estate()
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
                Category = new Category() { Id = -1, ApartmentType = EstateType.Flat }
            };
        }

        Task<IEnumerable<Advertisement>> IAdvertisementRepository.FindAllAsync(Expression<Func<Advertisement, bool>> condition, int quantity)
        {
            throw new NotImplementedException();
            // return Task.FromResult(new List<Advertisement> { new Advertisement()}.Select(a => a));
        }
    }
}
