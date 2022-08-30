using ApplicationCore.Models;
using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Data
{
    public static class NewPlaceDbInitializer
    {
        public static void Initialize(NewPlaceDb context)
        {
            context.Database.EnsureCreated();

            if (context.Advertisements.Count() > 10)
                return;

            Advertisement[] testData = GetTestData(); 
            context.Advertisements.AddRange(testData);

            context.SaveChanges();
        }

        private static Advertisement[] GetTestData()
        {
            var fakeForCategory = new Faker<Category>()
                .RuleFor(a => a.ApartmentType, (f, x) => f.Random.Enum<EstateType>())
                .RuleFor(a => a.PricingType, (f, x) => f.Random.Enum<PricingType>());

            var fakeForUser = new Faker<User>()
                .RuleFor(a => a.Login, (f, x) => f.Person.FirstName)
                .RuleFor(a => a.Email, (f, x) => f.Person.Email)
                .RuleFor(a => a.PasswordHash, (f, x) => f.Random.String());

            var fakeForCountry = new Faker <Country>()
                .RuleFor(a => a.Name, (f, x) => f.Address.Country());

            var fakeForLocation = new Faker<Location>()
                .RuleFor(a => a.Address, (f, x) => f.Address.FullAddress())
                .RuleFor(a => a.City, (f, x) => f.Address.City())
                .RuleFor(a => a.PostalCode, (f, x) => f.Address.SecondaryAddress())
                .RuleFor(a => a.Longitude, (f, x) => f.Address.Longitude())
                .RuleFor(a => a.Latitude, (f, x) => f.Address.Latitude())
                .RuleFor(a => a.Radius, (f, x) => f.Random.Double(min: 1, max: 50))
                .RuleFor(a => a.Country, (f, x) => fakeForCountry);

            var fakeForEstate = new Faker<Estate>()
                .RuleFor(a => a.Area, (f, x) => f.Random.Double(min: 20, max: 4000))
                .RuleFor(a => a.Utilities, (f, x) => new List<Utility> { new Utility(null, f.Commerce.ProductName(), f.Random.Decimal()) })
                .RuleFor(a => a.Location, (f, x) => fakeForLocation);

            var generatedData = new Faker<Advertisement>()
                //.StrictMode(true)
                .RuleFor(a => a.Title, (f, x) => f.Commerce.ProductName())
                .RuleFor(a => a.Description, (f, x) => f.Rant.Review())
                .RuleFor(a => a.CreateDate, (f, x) => f.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
                .RuleFor(a => a.ValidTo, (f, x) => f.Date.Between(DateTime.Now, DateTime.Now.AddDays(10)))
                .RuleFor(a => a.Price, (f, x) => decimal.Parse(f.Commerce.Price()))
                .RuleFor(a => a.Provision, (f, x) => decimal.Parse(f.Commerce.Price()))
                .RuleFor(a => a.Category, () => fakeForCategory)
                .RuleFor(a => a.User, () => fakeForUser)
                .RuleFor(a => a.Estate, () => fakeForEstate)
                .Generate(1000);

            var data = new Advertisement[]
                        {
                new Advertisement(
                    id: null,
                    title: "House for sale",
                    description: "Very luxurious house.",
                    createDate: DateTime.Now,
                    validTo: DateTime.Now + TimeSpan.FromDays(10),
                    estate: new Estate(
                        id: null,
                        area: 200,
                        utilities: new List<Utility>() { new Utility(id: null, name: "Electricity", cost: 300) },
                        location: new Location(
                            id: null,
                            address: "Józefa Szujskiego 5",
                            city: "Kraków",
                            postalCode: "31-123",
                            latitude: 50.06409489164344,
                            longitude:  19.928898998922403,
                            radius: 12,
                            country: new Country(1, "Poland")
                        )
                    ),
                    price: 20000000,
                    provision: 20000,
                    user: new User
                    (
                        id: null,
                        login: "AgencyNewPlace",
                        email: "aaa@b4b.com",
                        passwordHash: "!@#4d3g3",
                        agency: new Agency(id: null, name: "NewPlaceAgency", address: "Adress", information: "New Place Agency" )
                    ),
                    category: new Category(id: null, apartmentType: EstateType.House, pricingType: PricingType.Sale)
                ),

                new Advertisement(
                    id: null,
                    title: "Cheap room for rent",
                    description: "Cheap and comfortable room",
                    createDate: DateTime.Now,
                    validTo: DateTime.Now + TimeSpan.FromDays(14),
                    category: new Category(id: null, apartmentType: EstateType.Flat, pricingType: PricingType.Exchange),
                    estate: new Estate(
                        id: null,
                        area: 56,
                        utilities: new List<Utility>() { new Utility(id: null, name: "Internet", cost: 100) },
                        location: new Location(
                            id: null,
                            address: "Karmelicka 50",
                            city: "Kraków",
                            postalCode: "30-128",
                            latitude: 50.06717726700878,
                            longitude: 19.928392916221384,
                            radius: 132,
                            country: new Country(1, "Poland")
                        )
                    ),
                    price: 2000,
                    provision: 2000,
                    user: new User(
                        id: null,
                        login: "Matt",
                        passwordHash: "!@t$d3g3",
                        email: "matt@damon.com",
                        agency: null
                    )
                ),

                new Advertisement(
                    id: null,
                    title: "Flat for exchange!",
                    description: "Flat for exchange",
                    createDate: DateTime.Now,
                    validTo: DateTime.Now + TimeSpan.FromHours(10),
                    estate: new Estate(
                        id: null,
                        area: 40,
                        utilities: new List<Utility>() { new Utility(id: null, name : "Water", cost: 203) },
                        location: new Location(
                            id: null,
                            address: "Karmelicka 39",
                            city: "Kraków",
                            postalCode: "31-128",
                            longitude: 19.92885962055822,
                            latitude: 50.06721170060544,
                            radius: 12,
                            country: new Country(1, "Poland")
                        )
                    ),
                    price: 20000,
                    provision: 20000,
                    user: new User(
                        id: null,
                        login: "Agency",
                        passwordHash: "!@#$d3g3",
                        agency: new Agency(id: null, address: "tmp", information: "still tmp", name: "AgencyAgency" ),
                        email: "aaa@bbb.com"
                    ),
                    category: new Category(id: null, apartmentType: EstateType.Flat, pricingType: PricingType.Exchange)

                )
                        };

            return data.Concat(generatedData).ToArray();
        }
    }
}
