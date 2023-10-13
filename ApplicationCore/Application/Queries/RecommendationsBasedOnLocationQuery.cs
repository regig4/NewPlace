using System.Collections.Generic;
using NewPlace.ResourceRepresentations;

namespace ApplicationCore.Application.Queries
{
    public record RecommendationsBasedOnLocationQuery(double Latitude, double Longitude) : IQuery<List<AdvertisementDetailsRepresentation>>;
}
