using ApplicationCore.DTOs;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Converters
{
    public static class DtoConverters
    {
        public static Advertisement ToDomain(this AdvertisementDto dto)
        {
            // todo
            return new Advertisement(dto.Id, dto.Title, null, new Category(null, EstateTypeFromString(dto.EstateType), PricingTypeFromString(dto.PricingType)), 
                DateTime.Now, DateTime.Now + TimeSpan.FromHours(24), new User(null, "asdf", "asdf", "asdf", null), 
                new Estate(null, dto.EstateArea, new Location(null, dto.EstateAddress, "postal", dto.EstateCity, 3, 23,32, null), null), 10, 10);
        }

        public static AdvertisementDto ToDto(this Advertisement domain)
        {
            // todo
            return new AdvertisementDto();
        }

        public static AdvertisementDetailsDto ToDetailsDto(this Advertisement domain)
        {
            return new AdvertisementDetailsDto();
        }

        private static PricingType PricingTypeFromString(string p) =>
            p switch
            {
                "For Sale"      => PricingType.Sale,
                "Rent"          => PricingType.Rent,
                "For Exchange"  => PricingType.Exchange,
                _               => PricingType.Unknown
            };

        private static EstateType EstateTypeFromString(string e) =>
            e switch
            {
                "House"         => EstateType.House,
                "Flat"          => EstateType.Flat,
                "Room"          => EstateType.Room,
                _               => throw new Exception("Unknown estateType")
            };

    }
}
