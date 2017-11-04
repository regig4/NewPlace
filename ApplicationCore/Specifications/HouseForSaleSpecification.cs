using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace ApplicationCore.Specifications
{
    public class AdvertisementSpecification : Specification<Estate>
    {
        //public AdvertisementSpecification()
        //public override Expression<Func<Apartment, bool>> ToExpression()
        //{
        //    return apartment => (apartment.Category.ApartmentType == "House") && (apartment.Category.PricingType == "For sale");
        //}
        public override Expression<Func<Estate, bool>> ToExpression()
        {
            throw new NotImplementedException();
        }
    }
}
