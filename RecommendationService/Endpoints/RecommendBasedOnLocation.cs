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
        public async override Task<ActionResult<RecommendBasedOnLocationResponse>> HandleAsync(RecommendBasedOnLocationRequest request, CancellationToken cancellationToken = default)
        {
            var ads = await RecommendationsBasedOnLocationDao.Instance.GetAdvertisementsInRadius(request.Latitude, request.Longitude, request.Radius);
            return Ok(new RecommendBasedOnLocationResponse(ads));
        }
    }
}
