using ApplicationCore.DTOs;

namespace NewPlace.ResourceRepresentations
{
    public class AdvertisementRepresentation : Representation<AdvertisementDto>
    {
        public ImageRepresentation? Thumbnail { get; set; }
    }
}
