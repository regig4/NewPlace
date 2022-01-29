using Infrastructure;
using Infrastructure.Data;
using Infrastructure.Factories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddTransient(provider => AdvertisementApplicationServiceFactory.Instance.CreateAdvertisementService());

builder.Services.AddDbContext<NewPlaceDb>(options =>
    options.UseSqlServer(
        Infrastructure.Configuration.Configuration.DefaultConnectionString));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.


using (var serviceScope = app.Services.GetService<IServiceScopeFactory>().CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<NewPlaceDb>();
    NewPlaceDbInitializer.Initialize(context);
}


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
