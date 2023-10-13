using System;
using System.Collections.Generic;

namespace ApplicationCore.DTOs
{
    public class AdvertisementDetailsDto
    {

        public int? Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ApartmentType { get; set; }
        public string PricingType { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime ValidTo { get; set; } = DateTime.Now;
        public string UserName { get; set; }
        public double EstateArea { get; set; }
        public string EstateAddress { get; set; }
        public string EstateCity { get; set; }
        public List<(string utility, decimal? cost)> Utilities { get; set; }
        public decimal Price { get; set; }
        public decimal? Provision { get; set; }
        public string TotalCost { get; set; }
        private (decimal Cost, string Additionaly) UtilitesCost { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
