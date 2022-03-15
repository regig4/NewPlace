using ApplicationCore.Models;
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

            if (context.Advertisements.Any())
                return;

            Advertisement[] testData = GetTestData(); 
            context.Advertisements.AddRange(testData);

            context.SaveChanges();
        }

        private static Advertisement[] GetTestData()
        {
            return new Advertisement[]
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
        }
    }
}
