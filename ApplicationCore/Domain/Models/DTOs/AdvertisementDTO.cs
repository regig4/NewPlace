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
        public AdvertisementDto()
        {

        }
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
        }

        public int? Id { get; set; }
        public string Title { get; set; }
        public string EstateType { get; set; }
        public string PricingType { get; set; }
        public string UserName { get; set; }
        public double EstateArea { get; set; }
        public string EstateAddress { get; set; }
        public string EstateCity { get; set; }
        public List<string> Utilities { get; set; }
        public decimal Price { get; set; }
        public decimal? Provision { get; set; }
    }
}   
