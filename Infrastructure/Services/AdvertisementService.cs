using System;
using System.Collections.Generic;
using System.Text;
using ApplicationCore.Models;
using ApplicationCore.Services;
using Infrastructure.Repositories;

namespace Infrastructure.Services
{
    public class AdvertisementService : IAdvertisementService
    {
        private IAdvertisementRepository _repository;
        public AdvertisementService(IAdvertisementRepository repository)
        {
            _repository = repository;
        }

        public Advertisement GetById(int id)
        {
            return _repository.GetById(id);
        }
    }
}
