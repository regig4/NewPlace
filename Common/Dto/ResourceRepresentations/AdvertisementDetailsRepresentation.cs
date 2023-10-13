using System.Collections.Generic;
using ApplicationCore.DTOs;

namespace NewPlace.ResourceRepresentations
{
    public class AdvertisementDetailsRepresentation : Representation<AdvertisementDetailsDto>
    {
        public AdvertisementDetailsRepresentation() { }
        public ImageRepresentation Thumbnail { get; set; }
        private ICollection<ImageRepresentation> Images { get; set; }
    }
}
