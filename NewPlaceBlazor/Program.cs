using System.Net.Http;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using FisSst.BlazorMaps.DependencyInjection;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;
using NewPlaceBlazor.Utils;

namespace NewPlaceBlazor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            WebAssemblyHostBuilder builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddBlazoredLocalStorage();

            builder.Services.AddTransient(sp =>
            {
                IConfiguration configuration = sp.GetRequiredService<IConfiguration>();
                string baseUri = configuration.GetServiceUri("api", "https")?.ToString() ?? "https://localhost:44347/";

                HttpClient httpClient = new HttpClient(new JwtHttpClientHandler(baseUri, sp.GetRequiredService<ILocalStorageService>()));

                return new ApiClient(baseUri, httpClient);
            });

            builder.Services.AddMudServices();
            builder.Services.AddBlazorLeafletMaps();

            await builder.Build().RunAsync();
        }
    }
}
