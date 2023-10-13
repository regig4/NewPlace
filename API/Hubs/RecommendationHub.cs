using System;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.DTOs;
using ApplicationCore.Services;
using Common.Dto;
using Microsoft.AspNetCore.SignalR;

internal class RecommendationHub : Hub
{
    private readonly IRecommendationService service;
    private readonly IAdvertisementService advertisementService;

    public RecommendationHub(IRecommendationService service, IAdvertisementService advertisementService)
    {
        this.service = service;
        this.advertisementService = advertisementService;
    }

    public async Task RecommendTest()
    {
        await Clients.All.SendAsync("ReceiveMessage", "Hello");
        await Task.Delay(3000);
        await Clients.All.SendAsync("ReceiveMessage", "Hello world!");
    }

    public async Task RecommendBasedOnCurrentLocation(Guid userId, double longitude, double latitude)
    {
        SemaphoreSlim semaphore = new SemaphoreSlim(1);
        IObservable<AdvertisementDto> recommendationsStream = await service.RecommendByGeolocation(userId, new Location(longitude, latitude));
        //recommendationsStream.Subscribe(
        //    async (recommendation) => 
        //        await Clients.Caller.SendAsync("ReceiveRecommendation", 
        //                                        recommendation.ToRepresentation(recommendation.Id.ToString(), advertisementService)),
        //    () => 
        //    { 
        //        semaphore.Release(); 
        //    }
        //);
        await semaphore.WaitAsync();
    }
}