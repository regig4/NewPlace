using ApplicationCore.DTOs;
using ApplicationCore.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using Xunit;

namespace Tests.UnitTests
{
    public class MapperTests
    {
        [Fact]
        public void TestMap()
        {
            //// Arrange
            //var config = new MapperConfiguration(cfg =>
            //{
            //    cfg.CreateMap<Advertisement, ApplicationCore.DTOs.AdvertisementDetailsDto>()
            //        .ForMember(dest => dest.PricingType, opts => opts.MapFrom(src => src.Category.PricingType))
            //        .ForMember(dest => dest.ApartmentType, opts => opts.MapFrom(src => src.Category.ApartmentType))
            //        .ReverseMap();
            //});

            //var mapper = config.CreateMapper();

            //var domain = new Advertisement
            //(
            //    id: 1,
            //    validTo: DateTime.Now + TimeSpan.FromDays(10),
            //    description: "test",
            //    createDate: DateTime.Now,
            //    user: new ApplicationCore.Models.User(id: null, login: "login", passwordHash: "pass", email: "email", agency: null),
            //    category: new ApplicationCore.Models.Category(id: 1, pricingType: ApplicationCore.Models.PricingType.Rent, apartmentType: ApplicationCore.Models.EstateType.Flat),
            //    title: "AAA",
            //    price: 324,
            //    provision: null,
            //    estate: new Estate(id: null, area: 23,
            //        location: new Location(id: null, address: "tm", postalCode: "?", city: "ktk", latitude: 12, longitude: 23, radius: 12, country: new Country(1, "Poland")),
            //        utilities: new List<ApplicationCore.Models.Utility> { new ApplicationCore.Models.Utility(id: null, name: "name", cost: 12) }
            //));

            //var dto = domain.ToDto;

            //// Act
            //var mapped = mapper.Map<ApplicationCore.Models.Advertisement>(dto);

            //// Assert
            //Assert.Equal(mapped.Title, domain.Title);
            //Assert.Equal(mapped.Category.PricingType, domain.Category.PricingType);
            //Assert.Equal(mapped.Category.ApartmentType, domain.Category.ApartmentType);
        }
    }
}