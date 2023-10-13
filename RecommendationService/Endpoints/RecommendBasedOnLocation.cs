using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using RecommendationService.Data;

namespace RecommendationService.Endpoints
{
    public class RecommendBasedOnLocation : BaseAsyncEndpoint
                                                .WithRequest<RecommendBasedOnLocationRequest>
                                                .WithResponse<RecommendBasedOnLocationResponse>
    {
        [HttpPost("location")]
        public override async Task<ActionResult<RecommendBasedOnLocationResponse>> HandleAsync(RecommendBasedOnLocationRequest request, CancellationToken cancellationToken = default)
        {
            List<ApplicationCore.DTOs.AdvertisementDetailsDto>? ads = await RecommendationsBasedOnLocationDao.Instance.GetAdvertisementsInRadius(request.Latitude, request.Longitude, request.Radius);
            return Ok(new RecommendBasedOnLocationResponse(ads));
        }
    }
}
