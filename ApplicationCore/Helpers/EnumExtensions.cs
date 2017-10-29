using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Helpers
{
    public static class EnumExtensions
    {
        public static string ToFriendlyString(this ApartmentType apartmentType)
        {
            switch(apartmentType)
            {
                case ApartmentType.Flat:
                    return "Flat";
                case ApartmentType.Room:
                    return "Room";
                case ApartmentType.House:
                    return "House";
                default:
                    throw new ArgumentException("Invalid apartmentType");
            }
        }

        public static string ToFriendlyString(this PricingType pricingType)
        {
            switch (pricingType)
            {
                case PricingType.Exchange:
                    return "Exchange";
                case PricingType.Rent:
                    return "Rent";
                case PricingType.Sale:
                    return "Sale";
                case PricingType.Unknown:
                    return "Unknown";
                default:
                    throw new ArgumentException("Invalid pricingType");
            }
        }

        public static ApartmentType ToApartmentType(this string estateType)
        {
            switch (estateType)
            {
                case "flat":
                    return ApartmentType.Flat;
                case "room":
                    return ApartmentType.Room;
                case "house":
                    return ApartmentType.House;
                default:
                    return default(ApartmentType);
            }
        }
    }
}
