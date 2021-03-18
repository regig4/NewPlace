using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewPlace.ResourceRepresentations
{
    public class ImageRepresentation : Representation<string>
    {
        public string MediaType { get; set; }
    }
}
