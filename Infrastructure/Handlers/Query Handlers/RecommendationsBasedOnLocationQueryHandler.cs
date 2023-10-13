using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Application.Queries;
using ApplicationCore.DTOs;
using NewPlace.ResourceRepresentations;

namespace Infrastructure.Handlers.Query_Handlers
{
    public class RecommendationsBasedOnLocationQueryHandler : IQueryHandler<RecommendationsBasedOnLocationQuery, List<AdvertisementDetailsRepresentation>>
    {
        private readonly HttpClient _client;

        public RecommendationsBasedOnLocationQueryHandler(IHttpClientFactory clientFactory)
        {
            _client = clientFactory.CreateClient(nameof(RecommendationsBasedOnLocationQueryHandler));
        }

        public async Task<List<AdvertisementDetailsRepresentation>> Handle(RecommendationsBasedOnLocationQuery request, CancellationToken cancellationToken)
        {
            HttpResponseMessage? result = await _client.PostAsync($"location", new StringContent(JsonSerializer.Serialize(new
            {
                latitude = request.Latitude,
                longitude = request.Longitude,
                radius = 20
            }), Encoding.UTF8, "application/json"));

            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception("Status code from advertisement service was " + result.StatusCode);
            }

            string? stringContent = await result.Content.ReadAsStringAsync();
            Response? content = JsonSerializer.Deserialize<Response>(stringContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true, ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve });

            return content.Recommendations.Select(r => new AdvertisementDetailsRepresentation { Resource = r, Links = new List<Link>(), Thumbnail = new ImageRepresentation() }).ToList();
        }

        private record Response(List<AdvertisementDetailsDto> Recommendations);
    }
}
