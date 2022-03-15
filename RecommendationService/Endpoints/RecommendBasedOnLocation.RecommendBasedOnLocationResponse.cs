using ApplicationCore.DTOs;

namespace RecommendationService.Endpoints
{
    public record RecommendBasedOnLocationResponse(List<AdvertisementDetailsDto> recommendations);
}