using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;
using NewPlaceBlazor.Utils;
using Blazored.LocalStorage;
using FisSst.BlazorMaps.DependencyInjection;

namespace NewPlaceBlazor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddBlazoredLocalStorage();
            
            builder.Services.AddTransient(sp =>
            {
                var configuration = sp.GetRequiredService<IConfiguration>();
                string baseUri = configuration.GetServiceUri("api", "https")?.ToString() ?? "https://localhost:44347/";

                var httpClient = new HttpClient(/*new JwtHttpClientHandler(baseUri, sp.GetRequiredService<ILocalStorageService>())*/);

                return new ApiClient(baseUri, httpClient);
            });

            builder.Services.AddMudServices();
            builder.Services.AddBlazorLeafletMaps();

            await builder.Build().RunAsync();
        }
    }
}
