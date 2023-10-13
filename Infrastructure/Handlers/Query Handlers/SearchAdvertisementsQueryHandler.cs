using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Application.Queries;
using NewPlace.ResourceRepresentations;

namespace Infrastructure.Handlers.Query_Handlers
{
    public class SearchAdvertisementsQueryHandler : IQueryHandler<SearchAdvertisementsQuery, IReadOnlyList<AdvertisementRepresentation>>
    {
        private readonly HttpClient _client;

        public SearchAdvertisementsQueryHandler(IHttpClientFactory clientFactory)
        {
            _client = clientFactory.CreateClient(nameof(SearchAdvertisementsQueryHandler));
        }

        public async Task<IReadOnlyList<AdvertisementRepresentation>> Handle(SearchAdvertisementsQuery request, CancellationToken cancellationToken)
        {
            HttpResponseMessage? result = await _client.GetAsync($"Advertisement/search?estateType={request.EstateType}&city={request.City}&radius={request.Radius}");

            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception("Status code from advertisement service was " + result.StatusCode);
            }

            string? stringContent = await result.Content.ReadAsStringAsync();
            List<AdvertisementRepresentation>? ads = JsonSerializer.Deserialize<List<AdvertisementRepresentation>>(stringContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true, ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve });

            return ads;
        }
    }
}
