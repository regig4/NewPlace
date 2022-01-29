using System;
using System.Net.Http;
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
            var response = await base.SendAsync(new HttpRequestMessage(HttpMethod.Get, _baseUri + "authorization/login"), cancellationToken);
            var contentString = await response.Content.ReadAsStringAsync(cancellationToken);
            var token = JsonSerializer.Deserialize<string>(contentString);
            await _localStorage.SetItemAsStringAsync("token", token);
            return token;
        }
    }
}
