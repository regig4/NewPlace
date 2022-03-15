using System.Net.Http;
using System.Reflection;
using System.Text;
using ApplicationCore.Application.Behaviors;
using ApplicationCore.Application.Commands;
using ApplicationCore.Application.Services;
using ApplicationCore.Services;
using Infrastructure;
using Infrastructure.Factories;
using Infrastructure.Handlers.Query_Handlers;
using Infrastructure.Models.Commands;
using Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace API
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
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddMediatR(
                Assembly.GetExecutingAssembly(),
                Assembly.GetAssembly(typeof(ICommand)),
                Assembly.GetAssembly(typeof(DonateCommandHandler))
            );

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

            services.AddCors(options =>
            {
                options.AddPolicy("MyAllowSpecificOrigins",
                builder =>
                {
                    builder
                    //.WithOrigins("http://localhost:4200")
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });


            //services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseSqlServer(
            //        Configuration.GetConnectionString("DefaultConnection")));

            // Enables us to see error details from signalR 
            IdentityModelEventSource.ShowPII = true;

            services.AddLogging();

            services.AddReverseProxy().LoadFromConfig(Configuration.GetSection("ReverseProxy"));

            services.AddAuthentication()
                    .AddIdentityServerJwt();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters()
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = "http://localhost:44347/",//Configuration["Jwt:Issuer"], todo!
                            ValidAudience = "http://localhost:44381/",//Configuration["Jwt:Issuer"],
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),
                        };
                    });
            services.AddAuthentication(CertificateAuthenticationDefaults.AuthenticationScheme)
                    .AddCertificate();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            });

            services.AddHttpClient<IUserService, UserServiceProxy>(client =>
            {
                client.BaseAddress = Configuration.GetServiceUri("userservice");
            });

            services.AddHttpClient(nameof(SearchAdvertisementsQueryHandler), client =>
            {
                client.BaseAddress = Configuration.GetServiceUri("advertisementservice") ?? new System.Uri("https://localhost:7185");
            });

            services.AddHttpClient(nameof(RecommendationsBasedOnLocationQueryHandler), client =>
            {
                client.BaseAddress = Configuration.GetServiceUri("recommendationservice") ?? new System.Uri("https://localhost:7204");
            });

            services.AddTransient(provider => GrpcChannelFactory.Instance.Create());
            services.AddTransient(provider => RecommendationServiceFactory.Instance.Create(
                 provider.GetService<IAdvertisementService>(),
                 provider.GetService<IUserService>()));
            services.AddTransient<IMessageQueue>(provider => new MessageQueue());

            services.AddSignalR();
            services.AddControllers();
                //.AddJsonOptions(opts => opts.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve); 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
            }
            app.UseCors("MyAllowSpecificOrigins");

            app.UseHttpLogging();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<RecommendationHub>("/recommendationHub");
                endpoints.MapControllers();
                endpoints.MapReverseProxy();
            });
        }
    }
}
