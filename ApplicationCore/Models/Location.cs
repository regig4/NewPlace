using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Models
{
    public class Location
    {
        public int Id { get; set; }

        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }


        // Precise location:
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Accuracy { get; set; }
    }
}
