using ApplicationCore.Application.Services;
using ApplicationCore.Services;
using Infrastructure.Services;

namespace Infrastructure.Factories
{
    public sealed class RecommendationServiceFactory
    {
        private RecommendationServiceFactory() { }

        public static RecommendationServiceFactory Instance { get; } = new RecommendationServiceFactory();

        public IRecommendationService Create(IAdvertisementService advertisementService, IUserService userService)
        {
            return new RecommendationService(
                null,
                advertisementService,
                userService);
        }
    }
}
