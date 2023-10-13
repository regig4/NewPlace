using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PaymentService.ApplicationCore.Application.Repositories;
using PaymentService.ApplicationCore.Application.Services;
using PaymentService.Infrastructure.EventStream;
using PaymentService.Infrastructure.MessageQueue;

namespace PaymentService
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IMessageQueue>(provider => new MessageQueue());
            services.AddTransient<IEventQueue>(provider => new EventQueue());
            services.AddTransient<IEventStore>(provider => new EventStore());
            services.AddTransient<IEventRepository>(provider => new EventRepository(new EventStore(), new EventQueue()));
            services.AddTransient<IPaymentApplicationService>(provider =>
                new PaymentApplicationService(new MessageQueue(), new EventRepository(new EventStore(), new EventQueue())));
            services.AddGrpc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<Services.PaymentService>();

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
            });
        }
    }
}
