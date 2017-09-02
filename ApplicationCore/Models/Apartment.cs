using System;
using System.Collections.Generic;

namespace ApplicationCore.Models
{
    public class Apartment
    {
        public int Id { get; set; }

        public Category Category { get; set; }

        public double Area { get; set; }

        public virtual ICollection<Utility> Utilities{ get; set; }
    }
}
