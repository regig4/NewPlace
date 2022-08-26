using System;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Blazored.LocalStorage;

namespace NewPlaceBlazor.Utils
{
    public class JwtHttpClientHandler : HttpClientHandler
    {
        private readonly string _baseUri;
        private readonly ILocalStorageService _localStorage;

        public JwtHttpClientHandler(string baseUri, ILocalStorageService localStorage)
        {
            _baseUri = baseUri;
            _localStorage = localStorage;
        }

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            string token = await _localStorage.GetItemAsStringAsync("token");

            if (token == null)
			{
                token = await AuthenticateAsync(cancellationToken);
			}

            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);

            return await base.SendAsync(request, cancellationToken);
        }

        private async Task<string> AuthenticateAsync(CancellationToken cancellationToken)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, _baseUri + "api/authorization/login");
            UserRepresentation userRepresentation = new UserRepresentation
            {
                Password = "AAA"
            };
            request.Content = new StringContent(JsonSerializer.Serialize(userRepresentation), Encoding.UTF8, MediaTypeNames.Application.Json);
            var response = await base.SendAsync(request, cancellationToken);
            var contentString = await response.Content.ReadAsStringAsync(cancellationToken);
            var user = JsonSerializer.Deserialize<UserRepresentation>(contentString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            var token = user.Token; 
            await _localStorage.SetItemAsStringAsync("token", token);
            return token;
        }
    }
}
