using System.Collections.Generic;
using NewPlace.ResourceRepresentations;

namespace ApplicationCore.Application.Queries
{
    public record SearchAdvertisementsQuery(string EstateType, string City, double Radius) : IQuery<IReadOnlyList<AdvertisementRepresentation>>;
}
