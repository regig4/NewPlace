using System;
using System.Collections.Generic;
using System.Text;
using ApplicationCore.Models;
using ApplicationCore.Services;
using Infrastructure.Repositories;
using System.Linq;
using ApplicationCore.DTOs;
using ApplicationCore.Helpers;
using System.IO;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Infrastructure.Services
{
    public class AdvertisementService : IAdvertisementService
    {
        private IAdvertisementRepository _repository;
        private IImageService _imageService;

        public AdvertisementService(IAdvertisementRepository repository, IImageService imageService)
        {
            _repository = repository;
            _imageService = imageService;
        }

        public AdvertisementDetailsDto GetById(int id)
        {
            return new AdvertisementDetailsDto(_repository.GetById(id));
        }

        public async IAsyncEnumerable<AdvertisementDto> GetAllAsync()
        {
            var allAdvertisements = _repository.FindAsync(advertisement => true);
            await foreach (var advertisement in allAdvertisements)
                yield return new AdvertisementDto(advertisement);
        }

        public async Task<IEnumerable<AdvertisementDto>> GetAllPagedAsync(int page)
        {
            // TODO
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AdvertisementDto>> GetByCityAndEstateTypeAsync(string city, string estateType)
        {
            var advertisements = new List<AdvertisementDto>();
            Expression<Func<Advertisement, bool>> condition;

            if (city == null || estateType == null)
                condition = a => true;
            else
                condition = a => a.Estate.Location.City.ToLower() == city.ToLower()
                                 && a.Category.ApartmentType.ToFriendlyString().ToLower() == estateType.ToLower();
            
            await foreach (var a in (_repository.FindAsync(condition)))
                advertisements.Add(new AdvertisementDto(a));

            return advertisements;
        }   

        public async Task<string> GetThumbnailBase64(int id)
        {
            return await _imageService.GetBase64OfFileAsync(Path.Combine("..", "Infrastructure", "Content", "Images", id.ToString() + ".jpg"));
        }

        public async Task<int> Add(Advertisement advertisement, string thumbnailBase64)
        {
            int id = await _repository.Add(advertisement);
            await _imageService.SaveBase64Img(id + ".jpg", thumbnailBase64);
            return id;
        }
    }
}
