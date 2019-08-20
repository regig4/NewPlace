﻿using ApplicationCore.Models;
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

            context.Advertisements.AddRange(new Advertisement[]
            {
                new Advertisement(
                    id: null,
                    title: "House for sale - only 20000000$",
                    description: "coool",
                    createDate: DateTime.Now,
                    validityTime: TimeSpan.FromDays(10),
                    estate: new Estate(
                        id: null,
                        area: 200,
                        utilities: new List<Utility>() { new Utility(id: null, name: "Electricity", cost: 300) },
                        location: new Location(
                            id: null,
                            address: "Wesoła bardzo 24",
                            city: "Warszawa",
                            postalCode: "34-345",
                            latitude: 12,
                            longitude: 12,
                            radius: 12
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
                        agency: new Agency(id: null, name: "NewPlaceAgency", address: "notnull", information: "New Place Agency" )
                    ),
                    category: new Category(id: null, apartmentType: EstateType.House, pricingType: PricingType.Sale)
                ),

                new Advertisement(
                    id: null,
                    title: "Cheap room for rent",
                    description: "kinda cool",
                    createDate: DateTime.Now,
                    validityTime: TimeSpan.FromDays(14),
                    category: new Category(id: null, apartmentType: EstateType.Flat, pricingType: PricingType.Exchange),
                    estate: new Estate(
                        id: null,
                        area: 56,
                        utilities: new List<Utility>() { new Utility(id: null, name: "Internet", cost: 100) },
                        location: new Location(
                            id: null,
                            address: "Amelininymowa 12",
                            city: "Kraków",
                            postalCode: "4335",
                            latitude: 23,
                            longitude: 32,
                            radius: 132
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
                    title: "Flat for exchange - urgent!",
                    description: "it's ok",
                    createDate: DateTime.Now,
                    validityTime: TimeSpan.FromHours(10),
                    estate: new Estate(
                        id: null,
                        area: 40,
                        utilities: new List<Utility>() { new Utility(id: null, name : "Water", cost: 203) },
                        location: new Location(
                            id: null,
                            address: "Amelininymowa 13",
                            city: "Kraków",
                            postalCode: "4335",
                            longitude: 12,
                            latitude:12,
                            radius: 12
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
            });

            context.SaveChanges();
        }
    }
}
