using ApplicationCore.Helpers;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationCore.DTOs
{
    public class AdvertisementDto
    {
        public AdvertisementDto(Advertisement advertisement)
        {
            Id = advertisement.Id;
            Title = advertisement.Title;
            ApartmentType = advertisement.Category.ApartmentType.ToFriendlyString();
            PricingType = advertisement.Category.PricingType.ToFriendlyString();
            UserName = advertisement.User.Login;
            ApartmentAddress = advertisement.Apartment.Location?.Address;
            ApartmentArea = advertisement.Apartment.Area;
            ApartmentCity = advertisement.Apartment.Location?.City;
            Utilities = advertisement.Apartment.Utilities.Select(u => u.Name).ToList();
            Price = advertisement.Price;
            Provision = advertisement.Provision;
            TotalCost = advertisement.TotalCost;
        }

        public int? Id { get; }
        public string Title { get; }
        public string ApartmentType { get; }
        public string PricingType { get; }
        public string UserName { get; }
        public double ApartmentArea { get; }
        public string ApartmentAddress { get; }
        public string ApartmentCity { get; }
        public List<string> Utilities { get; }
        public decimal Price { get; }
        public decimal? Provision { get; }
        public string TotalCost { get; }
    }
}
