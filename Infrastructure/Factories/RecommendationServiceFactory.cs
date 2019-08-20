using ApplicationCore.Services;
using Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Factories
{
    public sealed class RecommendationServiceFactory
    {
        private RecommendationServiceFactory() { }

        public static RecommendationServiceFactory Instance { get; } = new RecommendationServiceFactory();

        public IRecommendationService Create()
        {
            return new RecommendationService(null, null);
        }
    }
}
