using FluentValidation;
using NewPlace.ResourceRepresentations;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Validators
{
    public class AdvertisementValidator : AbstractValidator<AdvertisementDetailsRepresentation>
    {
        public AdvertisementValidator()
        {
            RuleFor(x => x.Resource.Title).NotEmpty().WithMessage("Title is required");
            RuleFor(x => x.Resource.ApartmentType).NotEmpty().WithMessage("Category is required")
                .Matches("room|flat|house").WithMessage("Category must be room, flat or house");
            RuleFor(x => x.Resource.PricingType).NotEmpty().WithMessage("Pricing type is required")
                .Matches("for exchange|for sale|rent").WithMessage("Pricing must be for exchange, for sale or rent");
        }
    }
}
