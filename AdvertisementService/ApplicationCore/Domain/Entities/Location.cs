using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Models
{
    public class Location
    {
        public Location()
        {

        }

        public Location(double longitude, double latitude)
        {
            Longitude = Longitude;
            Latitude = latitude;
        }

        public Location(int? id, string address, string postalCode, string city, double latitude, double longitude, double radius, Country country)
            : this(id, address, postalCode, city, latitude, longitude, radius)
        {
            Country = country;
        }
        private Location(int? id, string address, string postalCode, string city, double latitude, double longitude, double radius)
        {
            Id = id;
            Address = address;
            PostalCode = postalCode;
            City = city;
            Latitude = latitude;
            Longitude = longitude;
            Radius = radius;
        }

        public int? Id { get; set; }

        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public Country Country { get; }


        // Precise location:
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Radius { get; set; }
        public double Accuracy { get; set; }
    }
}
