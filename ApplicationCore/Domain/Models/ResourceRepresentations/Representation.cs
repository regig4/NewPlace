using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewPlace.ResourceRepresentations
{
    public class Representation<T> 
    {
        public T Resource { get; set; }
        public IEnumerable<Link> Links { get; set; }
    }
}
