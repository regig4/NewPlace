using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace NewPlaceBlazor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");
            builder.Services.AddTransient(sp =>
            {
                return new HttpClient() { BaseAddress = new Uri("https://localhost:14148/") };
            });
            builder.Services.AddTransient(sp =>
            {
                var configuration = sp.GetRequiredService<IConfiguration>();
                return new ApiClient("https://localhost:14148/", new HttpClient());
            });

            await builder.Build().RunAsync();
        }
    }
}
