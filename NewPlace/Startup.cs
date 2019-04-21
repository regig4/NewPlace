using System.Text;
using Infrastructure;
using Infrastructure.Data;
using Infrastructure.Factories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace NewPlace
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),
                };
            });

//            var loggerFactory = LoggerFactory.Create(builder => {
//                builder.AddFilter("Microsoft", LogLevel.Warning)
//                       .AddFilter("System", LogLevel.Warning)
//                       .AddFilter("SampleApp.Program", LogLevel.Debug)
//                       .AddConsole();
//            }
//);
            services.AddTransient(provider => AdvertisementServiceFactory.Instance.CreateAdvertisementService());
            services.AddTransient(provider => AdvertisementServiceFactory.Instance.CreateAuthService());
            services.AddDbContext<NewPlaceDb>();
            services.AddSignalR();
            services.AddMvc(opt => opt.EnableEndpointRouting = false);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // database initialization
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<NewPlaceDb>();
                NewPlaceDbInitializer.Initialize(context);
            }

            // Automapper initialization
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<ApplicationCore.Models.Advertisement, ApplicationCore.DTOs.AdvertisementDetailsDto>();
                cfg.CreateMap<ApplicationCore.DTOs.AdvertisementDetailsDto, ApplicationCore.Models.Advertisement>();
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                //{
                //    HotModuleReplacement = true
                //});
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseAuthentication();

            app.UseStaticFiles();

            app.UseSignalR(routes =>
            {
                routes.MapHub<RecommendationHub>("/recommendationHub");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}
