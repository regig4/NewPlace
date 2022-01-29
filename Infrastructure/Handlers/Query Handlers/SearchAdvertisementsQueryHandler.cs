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

        public SearchAdvertisementsQueryHandler(HttpClient client)
        {
            _client = client;
        }

        public async Task<IReadOnlyList<AdvertisementRepresentation>> Handle(SearchAdvertisementsQuery request, CancellationToken cancellationToken)
        {
            var result = await _client.GetStringAsync($"search?estateType={request.EstateType}&city={request.City}&radius={request.Radius}");

            var ads = JsonSerializer.Deserialize<IReadOnlyList<AdvertisementRepresentation>>(result);

            return ads;
        }
    }
}
