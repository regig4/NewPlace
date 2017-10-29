using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Models
{
    public class Category
    {
        public int Id { get; set; }
        public ApartmentType ApartmentType { get; set; }
        public PricingType PricingType { get; set; }
    }
}
