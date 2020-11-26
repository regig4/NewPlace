using System;
using System.Collections.Generic;
using System.Text;
using ApplicationCore.Models;
using ApplicationCore.DTOs;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IAdvertisementService
    {
        AdvertisementDetailsDto GetById(int id);
        Task<IEnumerable<AdvertisementDto>> GetAllAsync();
        Task<IEnumerable<AdvertisementDto>> GetAllPagedAsync(int page);
        Task<IEnumerable<AdvertisementDto>> GetByCityAndEstateTypeAsync(string city, string estateType);
        Task<string> GetThumbnailBase64(int id);
        Task<int> Add(Advertisement advertisement, string thumbnailBase64);
    }
}
