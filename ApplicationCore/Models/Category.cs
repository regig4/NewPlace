using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Models
{
    public class Category
    {
        public Category(int? id, EstateType apartmentType, PricingType pricingType)
        {
            Id = id;
            ApartmentType = apartmentType;
            PricingType = pricingType;
        }

        public int? Id { get; set; }
        public EstateType ApartmentType { get; set; }
        public PricingType PricingType { get; set; }
    }
}
