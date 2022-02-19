using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewPlace.ResourceRepresentations;

namespace ApplicationCore.Application.Queries
{
    public record RecommendationsBasedOnLocationQuery(double latitude, double longitude) : IQuery<List<AdvertisementRepresentation>>;
}
