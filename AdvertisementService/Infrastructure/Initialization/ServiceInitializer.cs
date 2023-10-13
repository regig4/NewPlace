using AutoMapper;
using Infrastructure;
using Infrastructure.Data;

public class ServiceInitializer : IServiceInitializer
{
    private readonly NewPlaceDb _dbContext;

    public ServiceInitializer(NewPlaceDb dbContext)
    {
        _dbContext = dbContext;
    }

    public void Initialize()
    {
        // Database initialization
        NewPlaceDbInitializer.Initialize(_dbContext);

        // Automapper initialization (TODO: inject)
        MapperConfiguration? config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<ApplicationCore.Models.Advertisement, ApplicationCore.DTOs.AdvertisementDetailsDto>();
            cfg.CreateMap<ApplicationCore.DTOs.AdvertisementDetailsDto, ApplicationCore.Models.Advertisement>();
        });
    }
}
