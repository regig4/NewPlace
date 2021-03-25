using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace ApplicationCore.Specifications
{
    public class LocationInRangeSpecification : Specification<Estate>
    {
        private Location _location;
        private double _range;

        public LocationInRangeSpecification(Location location, double range)
        {
            _range = range;
            _location = location;
        }

        public override Expression<Func<Estate, bool>> ToExpression()
        {
            return apartment => Math.Abs(apartment.Location.Longitude - _location.Longitude) < _range
                                && Math.Abs(apartment.Location.Latitude - _location.Latitude) < _range;
        }
    }
}
