using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Application.Services;
using ApplicationCore.DTOs;
using ApplicationCore.Models;
using ApplicationCore.Services;
using NewPlace.ResourceRepresentations;

namespace Infrastructure.Services
{
    class RecommendationService : IRecommendationService
    {
        private readonly IGeolocationService _geolocationService;
        private readonly IAdvertisementService _advertisementService;
        private readonly IUserService _userService;

        public RecommendationService(IGeolocationService geolocationService, IAdvertisementService advertisementService, IUserService userService)
        {
            _geolocationService = geolocationService;
            _advertisementService = advertisementService;
            _userService = userService;
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

        public async Task<IObservable<AdvertisementDto>> RecommendByGeolocation(Guid userId, Location location)
        {
            var observedAdvertisements = await _userService.GetObservedAdvertisements(userId);

            var timer = Observable.Interval(TimeSpan.FromSeconds(3));
            var observable = Observable.Create<AdvertisementDto>(async (observer, cancelationToken) =>
                {
                    var getMostViewedByLocationTask = Task.Run(async () =>
                    {
                        await foreach (var r in _advertisementService.GetAllAsync())
                            if (!observedAdvertisements.Any(o => o.Id == r.Id))
                                observer.OnNext(r);
                    });

                    var getMostSimiliarToObservedByLocationTask = Task.Run(async () =>
                    {
                        await foreach (var r in _advertisementService.GetAllAsync())
                            if (!observedAdvertisements.Any(o => o.Id == r.Id))
                                observer.OnNext(r);
                    });

                    var getPromotedByLocationTask = Task.Run(async () =>
                    {
                        await foreach (var r in _advertisementService.GetAllAsync())
                            if (!observedAdvertisements.Any(o => o.Id == r.Id))
                                observer.OnNext(r);
                    });

                    await Task.WhenAll(getMostViewedByLocationTask, 
                                       getMostSimiliarToObservedByLocationTask, 
                                       getPromotedByLocationTask)
                                .ContinueWith(t => observer.OnCompleted());
                }
            );

            return observable.Zip(timer, (a, t) => a);
        }
    }
}
