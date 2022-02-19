using System.Collections.Generic;
using ApplicationCore.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
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

        [HttpGet]
        public IActionResult<List<AdvertisementRepresentation> GetRecommendationsBasedOnLocation(double latitude, double longitude) 
        {
            var recommendations = await _mediator.Send(new RecommendationsBasedOnLocationQuery(latitude, longitude));
            return Ok(recommendations);
        }
    }
}
