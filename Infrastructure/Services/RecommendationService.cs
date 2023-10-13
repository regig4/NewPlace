using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using ApplicationCore.Application.Services;
using ApplicationCore.DTOs;
using ApplicationCore.Services;
using Common.Dto;

namespace Infrastructure.Services
{
    public class RecommendationService : IRecommendationService
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

            IAsyncEnumerable<AdvertisementDto>? all = _advertisementService.GetAllAsync();
            IObservable<long>? timer = Observable.Interval(TimeSpan.FromSeconds(1));
            //var tmp = all.ToObservable();            // TODO: change to pagination
            //var tmp2 = tmp.Zip(timer, (a, t) => a);
            return null;
            // _advertisementService.GetFavourites(userId);
            // _advertisementService.GetRecentlyViewed();
            // _advertisementService.
        }

        public async Task<IObservable<AdvertisementDto>> RecommendByGeolocation(Guid userId, Location location)
        {
            List<AdvertisementDto>? observedAdvertisements = await _userService.GetObservedAdvertisements(userId);

            IObservable<long>? timer = Observable.Interval(TimeSpan.FromSeconds(3));
            IObservable<AdvertisementDto>? observable = Observable.Create<AdvertisementDto>(async (observer, cancelationToken) =>
                {
                    Task? getMostViewedByLocationTask = Task.Run(async () =>
                    {
                        await foreach (AdvertisementDto? r in _advertisementService.GetAllAsync())
                        {
                            if (!observedAdvertisements.Any(o => o.Id == r.Id))
                            {
                                observer.OnNext(r);
                            }
                        }
                    }, cancelationToken);

                    Task? getMostSimiliarToObservedByLocationTask = Task.Run(async () =>
                    {
                        await foreach (AdvertisementDto? r in _advertisementService.GetAllAsync())
                        {
                            if (!observedAdvertisements.Any(o => o.Id == r.Id))
                            {
                                observer.OnNext(r);
                            }
                        }
                    }, cancelationToken);

                    Task? getPromotedByLocationTask = Task.Run(async () =>
                    {
                        await foreach (AdvertisementDto? r in _advertisementService.GetAllAsync())
                        {
                            if (!observedAdvertisements.Any(o => o.Id == r.Id))
                            {
                                observer.OnNext(r);
                            }
                        }
                    }, cancelationToken);

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
