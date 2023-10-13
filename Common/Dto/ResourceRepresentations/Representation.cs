using System.Collections.Generic;

namespace NewPlace.ResourceRepresentations
{
    public class Representation<T>
    {
        public T Resource { get; set; }
        public IEnumerable<Link> Links { get; set; }
    }
}
