using System;
using System.Collections.Generic;

namespace ApplicationCore.Models
{
    public class Estate
    {
        public int Id { get; set; }

        public double Area { get; set; }
        
        public Location Location { get; set; }

        public virtual ICollection<Utility> Utilities{ get; set; }
    }
}
