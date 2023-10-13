using System.Linq.Expressions;
using ApplicationCore.Models;

namespace ApplicationCore.Specifications
{
    public class LocationInRangeSpecification : Specification<Estate>
    {
        private readonly Location _location;
        private readonly double _range;

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
