using System;
using System.Collections.Generic;
using System.Text;
using ApplicationCore.Models;
using ApplicationCore.DTOs;

namespace ApplicationCore.Services
{
    public interface IAdvertisementService
    {
        AdvertisementDetailsDto GetById(int id);
        IEnumerable<AdvertisementDto> GetAll();
        IEnumerable<AdvertisementDto> GetByCityAndEstateType(string city, string estateType);
        string GetThumbnailBase64(int id);
    }
}
