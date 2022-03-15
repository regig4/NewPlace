namespace RecommendationService.Endpoints
{
    public record RecommendBasedOnLocationRequest(double Latitude, double Longitude, double Radius);

}