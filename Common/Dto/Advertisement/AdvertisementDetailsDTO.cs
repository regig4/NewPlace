using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.DTOs.Advertisement
{
    public record AdvertisementDetailsDto
    (
        int? Id,
        string Title,
        string Description,
        string ApartmentType,
        string PricingType,
        DateTime CreateDate,
        DateTime ValidTo,
        string UserName,
        double EstateArea,
        string EstateAddress,
        string EstateCity,
        List<(string name, decimal? cost, string additionaly)> Utilities,
        decimal Price,
        decimal? Provision,
        string TotalCost
    );
}
