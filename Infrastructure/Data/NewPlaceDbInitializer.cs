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

            context.Advertisements.AddRange(new Advertisement[]
            {
                new Advertisement()
                {
                    Apartment = new Apartment()
                    {
                        Area = 200,
                        Category = new Category() { Name = "House for sale" },
                        Utilities = new List<Utility>() { new Utility() { Name = "Electricity", Cost = 300 } }
                    },
                    Price = 20000000,
                    Provision = 20000,
                    User = new User()
                    {
                        Login = "AgencyNewPlace",
                        Email = "aaa@b4b.com",
                        PasswordHash = "!@#4d3g3",
                        Agency = new Agency() { Name = "NewPlaceAgency", Address = null, Information = "New Place Agency" }
                    }
                },

                new Advertisement()
                {
                    Apartment = new Apartment()
                    {
                        Area = 56,
                        Category = new Category() { Name = "Apartment for rent" },
                        Utilities = new List<Utility>() { new Utility() { Name = "Internet", Cost = 100 } }
                    },
                    Price = 2000,
                    Provision = 2000,
                    User = new User()
                    {
                        Login = "Matt",
                        PasswordHash = "!@t$d3g3",
                        Email = "matt@damon.com"
                    }
                },

                new Advertisement()
                {
                    Apartment = new Apartment()
                    {
                        Area = 40,
                        Category = new Category() { Name = "Apartment for sale" },
                        Utilities = new List<Utility>() { new Utility() { Name = "Water", Cost = 203} }
                    },
                    Price = 20000,
                    Provision = 20000,
                    User = new User()
                    {
                        Login = "Agency",
                        PasswordHash = "!@#$d3g3",
                        Agency = new Agency() { Name = "AgencyAgency" },
                        Email = "aaa@bbb.com"
                    }
                }
            });

            context.SaveChanges();
        }
    }
}
