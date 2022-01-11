using System;
using System.Collections.Generic;
using System.Text;
using ApplicationCore.Models;
using ApplicationCore.DTOs;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IRecommendationService
    {
        Task<IObservable<AdvertisementDto>> RecommendByGeolocation(Guid userId, Location location);
    }
}
