using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.DTOs;
using ApplicationCore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

[Authorize]
class RecommendationHub : Hub 
{
    private readonly IRecommendationService service;

    public RecommendationHub(IRecommendationService service)
    {
        this.service = service;
    }

    public async Task RecommendTest() 
    {
        await Clients.All.SendAsync("ReceiveMessage", "Hello");
        await Task.Delay(3000);
        await Clients.All.SendAsync("ReceiveMessage", "Hello world!");
    }
}