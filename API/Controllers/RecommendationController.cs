using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NewPlace.ResourceRepresentations;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecommendationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RecommendationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("location")]
        public async Task<ActionResult<List<AdvertisementDetailsRepresentation>>> GetRecommendationsBasedOnLocation(double latitude, double longitude)
        {
            List<AdvertisementDetailsRepresentation> recommendations = await _mediator.Send(new RecommendationsBasedOnLocationQuery(latitude, longitude));
            return Ok(recommendations);
        }
    }
}
