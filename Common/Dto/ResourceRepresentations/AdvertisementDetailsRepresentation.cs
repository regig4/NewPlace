using ApplicationCore.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewPlace.ResourceRepresentations
{
    public class AdvertisementDetailsRepresentation : Representation<AdvertisementDetailsDto>
    {
        public AdvertisementDetailsRepresentation() { }
        public ImageRepresentation Thumbnail { get; set; }
        ICollection<ImageRepresentation> Images { get; set; }
    }
}
