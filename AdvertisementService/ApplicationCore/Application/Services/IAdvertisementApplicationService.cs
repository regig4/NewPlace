using ApplicationCore.DTOs;
using ApplicationCore.Models;

namespace AdvertisementService.ApplicationCore.Application.Services
{
    public interface IAdvertisementApplicationService
    {
        AdvertisementDetailsDto GetById(int id);
        IAsyncEnumerable<AdvertisementDto> GetAllAsync();
        Task<IEnumerable<AdvertisementDto>> GetAllPagedAsync(int page);
        Task<IEnumerable<AdvertisementDto>> GetByCityAndEstateTypeAsync(string? city, string? estateType);
        Task<string> GetThumbnailBase64(int id);
        Task<int> Add(Advertisement advertisement, string thumbnailBase64);
    }
}
