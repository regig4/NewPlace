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
        IObservable<AdvertisementDto> RecommendByGeolocation(int userId, Location location);
    }
}
