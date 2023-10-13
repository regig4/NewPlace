using System.Collections.Generic;

namespace ApplicationCore.DTOs
{
    public class AdvertisementDto
    {
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
