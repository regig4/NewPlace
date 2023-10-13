using System;
using System.Threading.Tasks;
using ApplicationCore.DTOs;
using Common.Dto;

namespace ApplicationCore.Services
{
    public interface IRecommendationService
    {
        Task<IObservable<AdvertisementDto>> RecommendByGeolocation(Guid userId, Location location);
    }
}
