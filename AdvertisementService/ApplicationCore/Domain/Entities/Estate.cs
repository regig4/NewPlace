using System;
using System.Collections.Generic;

namespace ApplicationCore.Models
{
    public class Estate
    {
        public Estate()
        {
            // for Automapper
        }

        private Estate(int? id, double area)
        {
            Id = id;
            Area = area;
        }

        public Estate(int? id, double area, Location location, ICollection<Utility> utilities) : this(id, area)
        {
            Location = location;
            Utilities = utilities;
        }

        public int? Id { get; set; }

        public double Area { get; set; }
        
        public Location Location { get; set; }

        public virtual ICollection<Utility> Utilities{ get; set; }
    }
}
