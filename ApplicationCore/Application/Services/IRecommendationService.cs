using System;
using System.Collections.Generic;
using System.Text;
using ApplicationCore.DTOs;
using System.Threading.Tasks;
using Common.Dto;

namespace ApplicationCore.Services
{
    public interface IRecommendationService
    {
        Task<IObservable<AdvertisementDto>> RecommendByGeolocation(Guid userId, Location location);
    }
}
