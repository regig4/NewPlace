using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Helpers
{
    public static class EnumExtensions
    {
        public static string ToFriendlyString(this EstateType apartmentType)
        {
            switch(apartmentType)
            {
                case EstateType.Flat:
                    return "Flat";
                case EstateType.Room:
                    return "Room";
                case EstateType.House:
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

        public static EstateType ToApartmentType(this string estateType)
        {
            switch (estateType)
            {
                case "flat":
                    return EstateType.Flat;
                case "room":
                    return EstateType.Room;
                case "house":
                    return EstateType.House;
                default:
                    return default(EstateType);
            }
        }
    }
}
