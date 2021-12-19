using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.ML;
using Microsoft.ML.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
       new WeatherForecast
       (
           DateTime.Now.AddDays(index),
           Random.Shared.Next(-20, 55),
           summaries[Random.Shared.Next(summaries.Length)]
       ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");



app.MapGet("/test-prediction", (float area) =>
{
    var mlContext = new MLContext();

    using var db = new NewPlaceDb();

    var data = db.Advertisements.Include(a => a.Estate).Select(a => new EstateData((float) a.Estate.Area, (float) a.Price)).ToList();

    IDataView trainingData = mlContext.Data.LoadFromEnumerable<EstateData>(data);

    var pipeline = mlContext.Transforms.Concatenate("Features", new[] { "Area" })
                   .Append(mlContext.Regression.Trainers.Sdca(labelColumnName: "Price", maximumNumberOfIterations: 100));

    var model = pipeline.Fit(trainingData);

    var test = new EstateData(area, 0);

    var price = mlContext.Model.CreatePredictionEngine<EstateData, Prediction>(model).Predict(test);

    return price;
})
.WithName("TestPrediction");



app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

record EstateData(float Area, float Price);

class Prediction
{
    [ColumnName("Score")] 
    public float Price { get; set; }
}





