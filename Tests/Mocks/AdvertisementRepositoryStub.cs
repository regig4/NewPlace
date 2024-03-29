﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ApplicationCore.Models;
using Infrastructure.Repositories;

namespace UnitTests.Mocks
{
    internal class AdvertisementRepositoryStub : IAdvertisementRepository
    {
        public Task<int> Add(Advertisement advertisement)
        {
            throw new NotImplementedException();
        }

        public Advertisement FindFirstOrDefault(Func<Advertisement, bool> condition)
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
            return new Advertisement(

                estate: new Estate(
                    area: 56,
                    id: -1,
                    utilities: new List<Utility>() { new Utility(id: null, name: "TestUtility", cost: 200) },
                    location: new Location(id: null, address: "stub", postalCode: "wow", city: "tmp", latitude: 12, longitude: 12, radius: 12, new Country(1, "Poland"))
                ),
                title: "test",
                description: "tetstest",
                createDate: DateTime.Now,
                validTo: DateTime.Now + TimeSpan.FromSeconds(20),
                id: -1,
                price: 2000,
                provision: 2000,
                user: new User(
                    id: -1,
                    login: "tmplogin",
                    passwordHash: "null",
                    email: "email",
                    agency: new Agency(id: -1, name: "TestAgency", address: "TestAddress", information: "TestInformation")
                ),
                category: new Category(id: -1, apartmentType: EstateType.Flat, pricingType: PricingType.Rent)
            );
        }

        IAsyncEnumerable<Advertisement> IAdvertisementRepository.FindAsync(Expression<Func<Advertisement, bool>> condition, int quantity)
        {
            throw new NotImplementedException();
            // return Task.FromResult(new List<Advertisement> { new Advertisement()}.Select(a => a));
        }
    }
}
