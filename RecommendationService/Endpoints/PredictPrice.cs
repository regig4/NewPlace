using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ML;
using Microsoft.ML.Data;

namespace RecommendationService.Endpoints;

public class PredictPrice : BaseAsyncEndpoint
                                .WithRequest<PredictPriceRequest>
                                .WithResponse<PredictPriceResponse>
{
    [HttpGet("predict-price")]
    public async override Task<ActionResult<PredictPriceResponse>> HandleAsync([FromQuery] PredictPriceRequest request, CancellationToken cancellationToken = default)
    {
        var mlContext = new MLContext();

        //using var db = new NewPlaceDb();

        //var data = await db.Advertisements
        //                .Include(a => a.Estate)
        //                .Select(
        //                            a => new EstateData
        //                                (
        //                                (float) a.Estate.Area, 
        //                                (int)   a.Category.ApartmentType,
        //                                (int)   a.Category.PricingType,
        //                                (float) a.Price
        //                                )
        //                        )
        //                .ToListAsync(cancellationToken: cancellationToken);

        var data = GetData();

        float minArea = data.Select(x => x.Area).Min();
        float maxArea = data.Select(x => x.Area).Max();

        float maxPrice = data.Select(x => x.Price).Max();
        float minPrice = data.Select(x => x.Price).Min();

        data = NormalizeValues(data);

        IDataView trainingData = mlContext.Data.LoadFromEnumerable(data);

        var pipeline = mlContext.Transforms.Concatenate("Features", new[] { nameof(EstateData.Area), nameof(EstateData.EstateType), nameof(EstateData.PricingType) })
                       .Append(mlContext.Regression.Trainers.Sdca(labelColumnName: "Price", maximumNumberOfIterations: 100));
        
        var model = pipeline.Fit(trainingData);

        float normalizedArea = (request.Area - minArea) / (maxArea - minArea);
        float normalizedEstateType = request.EstateType switch { 0 => 0.1f, 1 => 0.5f, _ => 0.9f };
        float normalizedPricingType = request.PricingType switch { 0 => 0.25f, _ => 0.75f };

        var test = new EstateData(normalizedArea, normalizedEstateType, normalizedPricingType, 0);

        var prediction = mlContext.Model.CreatePredictionEngine<EstateData, Prediction>(model).Predict(test);

        var denormalizedPrice = prediction.Price * (maxPrice - minPrice) + minPrice;

        return Ok(new PredictPriceResponse(denormalizedPrice));
    }

    private List<EstateData> GetData()
    {
        var random = new Random();
        var houses = Enumerable.Range(0, 1000).Select(e => 
        {
            var area = random.Next(100, 10000);
            return new EstateData(area, 0, e % 2, e % 2 == 0 ? (area % 100 + 1) * random.Next(500000, 3000000) : (area % 100 + 1) * random.Next(250, 2000));
        }
        );
        var flats = Enumerable.Range(0, 1000).Select(e => 
        {
            var area = random.Next(30, 100);
            return new EstateData(area, 1, e % 2, e % 2 == 0 ? (area % 30 + 1) * random.Next(100000, 1000000) : (area % 30 + 1) * random.Next(100, 500));
        }
        );
        var rooms = Enumerable.Range(0, 1000).Select(e => 
        {
            var area = random.Next(5, 40);
            return new EstateData(area, 2, e % 2, e % 2 == 0 ? (area % 5 + 1) * random.Next(50000, 100000) : (area % 5 + 1) * random.Next(50, 100));
        }
        );
        var result = new List<EstateData>();
        result.AddRange(houses);
        result.AddRange(flats);
        result.AddRange(rooms);
        return result;
    }

    private List<EstateData> NormalizeValues(List<EstateData> result)
    {
        float maxPrice = result.Select(e => e.Price).Max();
        float minPrice = result.Select(e => e.Price).Min();
        float maxArea = result.Select(e => e.Area).Max();
        float minArea = result.Select(e => e.Area).Min();
        return result.Select(e => new EstateData((e.Area - minArea) / (maxArea - minArea), 
                                                 e.EstateType switch { 0 => 0.1f, 1 => 0.5f, _ => 0.9f }, 
                                                 e.PricingType switch { 0 => 0.25f, _ => 0.75f },
                                                 (e.Price - minPrice) / (maxPrice - minPrice))).ToList();
    }

    record EstateData(float Area, float EstateType, float PricingType, float Price);

    class Prediction
    {
        [ColumnName("Score")]
        public float Price { get; set; }
    }

}

