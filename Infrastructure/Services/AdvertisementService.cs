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

        public IEnumerable<AdvertisementDto> GetAll()
        {
            return _repository.FindAll(advertisement => true).Select(a => new AdvertisementDto(a));
        }

        public IEnumerable<AdvertisementDto> GetByCityAndEstateType(string city, string estateType)
        {
            return _repository.FindAll(a => a.Estate.Location.City.ToLower() == city.ToLower() 
                && a.Category.ApartmentType.ToFriendlyString().ToLower() == estateType.ToApartmentType().ToFriendlyString().ToLower())
                .Select(a => new AdvertisementDto(a));
        }

        public string GetThumbnailBase64(int id)
        {
            return _imageService.GetBase64OfFile(Path.Combine("..", "Infrastructure", "Content", "Images", id.ToString() + ".jpg"));
        }
    }
}
