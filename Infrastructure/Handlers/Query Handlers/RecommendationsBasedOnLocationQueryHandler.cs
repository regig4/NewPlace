using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Application.Queries;
using NewPlace.ResourceRepresentations;

namespace Infrastructure.Handlers.Query_Handlers
{
    public class RecommendationsBasedOnLocationQueryHandler : IQueryHandler<RecommendationsBasedOnLocationQuery, List<AdvertisementRepresentation>>
    {
        public Task<List<AdvertisementRepresentation>> Handle(RecommendationsBasedOnLocationQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
