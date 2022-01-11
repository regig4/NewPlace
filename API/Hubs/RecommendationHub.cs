using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.DTOs;
using ApplicationCore.Models;
using ApplicationCore.Services;
using Infrastructure.Converters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using NewPlace.ResourceRepresentations;

class RecommendationHub : Hub 
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
        var semaphore = new SemaphoreSlim(1);
        var recommendationsStream = await service.RecommendByGeolocation(userId, new Location(longitude, latitude));
        recommendationsStream.Subscribe(
            async (recommendation) => 
                await Clients.Caller.SendAsync("ReceiveRecommendation", 
                                                recommendation.ToRepresentation(recommendation.Id.ToString(), advertisementService)),
            () => 
            { 
                semaphore.Release(); 
            }
        );
        await semaphore.WaitAsync();
    }
}