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
            EstateType = advertisement.Category.ApartmentType.ToFriendlyString();
            PricingType = advertisement.Category.PricingType.ToFriendlyString();
            UserName = advertisement.User.Login;
            EstateAddress = advertisement.Estate.Location?.Address;
            EstateArea = advertisement.Estate.Area;
            EstateCity = advertisement.Estate.Location?.City;
            Utilities = advertisement.Estate.Utilities.Select(u => u.Name).ToList();
            Price = advertisement.Price;
            Provision = advertisement.Provision;
            TotalCost = advertisement.TotalCost;
        }

        public int? Id { get; }
        public string Title { get; }
        public string EstateType { get; }
        public string PricingType { get; }
        public string UserName { get; }
        public double EstateArea { get; }
        public string EstateAddress { get; }
        public string EstateCity { get; }
        public List<string> Utilities { get; }
        public decimal Price { get; }
        public decimal? Provision { get; }
        public string TotalCost { get; }
    }
}
