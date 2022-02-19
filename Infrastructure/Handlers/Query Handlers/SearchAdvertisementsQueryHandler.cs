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
    public class SearchAdvertisementsQueryHandler : IQueryHandler<SearchAdvertisementsQuery, IReadOnlyList<AdvertisementRepresentation>>
    {
        private readonly HttpClient _client;

        public SearchAdvertisementsQueryHandler(IHttpClientFactory clientFactory)
        {
            _client = clientFactory.CreateClient(nameof(SearchAdvertisementsQueryHandler));
        }

        public async Task<IReadOnlyList<AdvertisementRepresentation>> Handle(SearchAdvertisementsQuery request, CancellationToken cancellationToken)
        {
            var result = await _client.GetAsync($"Advertisement/search?estateType={request.EstateType}&city={request.City}&radius={request.Radius}");

            if (result.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("Status code from advertisement service was " + result.StatusCode);

            var stringContent = await result.Content.ReadAsStringAsync();
            var ads = JsonSerializer.Deserialize<IReadOnlyList<AdvertisementRepresentation>>(stringContent);

            return ads;
        }
    }
}
