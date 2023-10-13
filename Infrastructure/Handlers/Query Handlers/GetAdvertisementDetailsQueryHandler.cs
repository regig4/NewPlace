using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Application.Queries;
using NewPlace.ResourceRepresentations;

namespace Infrastructure.Handlers.Query_Handlers
{
    public class GetAdvertisementDetailsQueryHandler : IQueryHandler<GetAdvertisementDetailsQuery, AdvertisementDetailsRepresentation>
    {
        private readonly HttpClient _client;

        public GetAdvertisementDetailsQueryHandler(IHttpClientFactory clientFactory)
        {
            _client = clientFactory.CreateClient(nameof(SearchAdvertisementsQueryHandler));
        }

        public async Task<AdvertisementDetailsRepresentation> Handle(GetAdvertisementDetailsQuery request, CancellationToken cancellationToken)
        {
            HttpResponseMessage? result = await _client.GetAsync($"Advertisement/{request.Id}");

            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception("Status code from advertisement service was " + result.StatusCode);
            }

            string? stringContent = await result.Content.ReadAsStringAsync();
            AdvertisementDetailsRepresentation? ad = JsonSerializer.Deserialize<AdvertisementDetailsRepresentation>(stringContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true, ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve });

            return ad;
        }
    }
}
