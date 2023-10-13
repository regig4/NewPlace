using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.DTOs;

namespace ApplicationCore.Services
{
    public interface IAdvertisementService
    {
        AdvertisementDetailsDto GetById(int id);
        IAsyncEnumerable<AdvertisementDto> GetAllAsync();
        Task<IEnumerable<AdvertisementDto>> GetAllPagedAsync(int page);
        Task<IEnumerable<AdvertisementDto>> GetByCityAndEstateTypeAsync(string city, string estateType);
        Task<string> GetThumbnailBase64(int id);
        Task<int> Add(AdvertisementDto advertisement, string thumbnailBase64);
    }
}