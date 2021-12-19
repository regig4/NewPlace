using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.DTOs;
using ApplicationCore.Models;
using ApplicationCore.Services;

namespace Infrastructure.Services
{
    class RecommendationService : IRecommendationService
    {
        private readonly IGeolocationService _geolocationService;
        private readonly IAdvertisementService _advertisementService;

        public RecommendationService(IGeolocationService geolocationService, IAdvertisementService advertisementService)
        {
            _geolocationService = geolocationService;
            _advertisementService = advertisementService;
        }

        public IEnumerable<AdvertisementDto> RecommendByGeolocation(int userId, Location location)
        {
            
            var all = _advertisementService.GetAllAsync();
            var timer = Observable.Interval(TimeSpan.FromSeconds(1));
            //var tmp = all.ToObservable();            // TODO: change to pagination
            //var tmp2 = tmp.Zip(timer, (a, t) => a);
            return null;
            // _advertisementService.GetFavourites(userId);
            // _advertisementService.GetRecentlyViewed();
            // _advertisementService.
        }

        IObservable<AdvertisementDto> IRecommendationService.RecommendByGeolocation(int userId, Location location)
        {
            throw new NotImplementedException();
        }

        // public void RecommendUsingMachineLearning
    }
}
