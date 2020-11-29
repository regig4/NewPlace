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
        public AdvertisementDetailsDto() { }
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

        public int? Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ApartmentType { get; set; }
        public string PricingType { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ValidTo { get; set; }
        public string UserName { get; set; }
        public double EstateArea { get; set; }
        public string EstateAddress { get; set; }
        public string EstateCity { get; set; }
        public List<(string utility, decimal? cost)> Utilities { get; set; }
        public decimal Price { get; set; }
        public decimal? Provision { get; set; }
        public string TotalCost { get; set; }
        private (decimal Cost, string Additionaly) UtilitesCost { get; set; }
    }
}
