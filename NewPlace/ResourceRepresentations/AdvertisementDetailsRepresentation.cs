using ApplicationCore.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewPlace.ResourceRepresentations
{
    public class AdvertisementDetailsRepresentation : Representation<AdvertisementDetailsDto>
    {
        ICollection<ImageRepresentation> Images { get; set; }
    }
}
