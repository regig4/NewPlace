using NewPlace.ResourceRepresentations;

namespace ApplicationCore.Application.Queries
{
    public record GetAdvertisementDetailsQuery(int Id) : IQuery<AdvertisementDetailsRepresentation>;
}
