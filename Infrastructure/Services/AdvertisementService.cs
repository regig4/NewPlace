﻿using System;
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

        public async Task<IEnumerable<AdvertisementDto>> GetAllAsync()
        {
            //var allAdvertisements = await _repository.FindAllAsync(advertisement => true);
            //return allAdvertisements.Select(a => new AdvertisementDto(a));
            return null;
        }

        public async Task<IEnumerable<AdvertisementDto>> GetAllPagedAsync(int page)
        {
            // TODO
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AdvertisementDto>> GetByCityAndEstateTypeAsync(string city, string estateType)
        {
            var advertisements = new List<Advertisement>();

            if (city == null || estateType == null)
                await foreach (var a in (_repository.FindAsync(a => 1 == 1)))
                    advertisements.Add(a);
            else
                await foreach (var a in (_repository.FindAsync(a =>
                a.Estate.Location.City.ToLower() == city.ToLower()
                 && a.Category.ApartmentType.ToFriendlyString().ToLower() == estateType.ToLower())))
                    advertisements.Add(a);

            return advertisements.Select(a => new AdvertisementDto(a));
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
