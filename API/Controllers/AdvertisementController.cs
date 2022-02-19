using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewPlace.ResourceRepresentations;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdvertisementController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AdvertisementController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("search")]
        public async Task<List<AdvertisementRepresentation>> Search(string estateType, string city, double radius)
        {
            var result = await _mediator.Send(new SearchAdvertisementsQuery(estateType, city, radius));
            return result.ToList();
        }

        [Authorize]
        [HttpPost("create")]
        public ActionResult<int> Create(AdvertisementDetailsRepresentation dto)
        {
            throw new NotImplementedException();
            //int id = _mediator.Send(new CreateAdvertisementCommand(dto.Resource, dto.Thumbnail));
            //return Ok(id);
        }
    }
}
