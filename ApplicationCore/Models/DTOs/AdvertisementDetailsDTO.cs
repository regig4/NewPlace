using ApplicationCore.Helpers;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationCore.DTOs
{
    public class AdvertisementDetailsDto
    {
        public AdvertisementDetailsDto(Advertisement advertisement)
        {
            Id = advertisement.Id;
            Title = advertisement.Title;
            Description = advertisement.Description;
            ApartmentType = advertisement.Category.ApartmentType.ToFriendlyString();
            PricingType = advertisement.Category.PricingType.ToFriendlyString();
            CreateDate = advertisement.CreateDate;
            ValidTo = advertisement.ValidTo;
            UserName = advertisement.User?.Login;
            EstateAddress = advertisement.Estate?.Location?.Address;
            EstateArea = advertisement.Estate == null ? 0 : advertisement.Estate.Area;
            EstateCity = advertisement.Estate?.Location?.City;
            Utilities = advertisement.Estate.Utilities.Select(util => (util.Name, util.Cost)).ToList();
            // TODO: rewrite rest
        }

        public int? Id { get; }
        public string Title { get; }
        public string Description { get; }
        public string ApartmentType { get; }
        public string PricingType { get; }
        public DateTime CreateDate { get; }
        public DateTime ValidTo { get; }
        public string UserName { get; }
        public double EstateArea { get; }
        public string EstateAddress { get; }
        public string EstateCity { get; }
        public List<(string utility, decimal? cost)> Utilities { get; } 
        public decimal Price { get; }
        public decimal? Provision { get; }
        public string TotalCost { get; }
        private (decimal Cost, string Additionaly) UtilitesCost { get; }
    }
}
