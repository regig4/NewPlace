using ApplicationCore.DTOs;
using Microsoft.AspNetCore.Mvc;
using NewPlace.ResourceRepresentations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Net.Http;
using System.IO;
using Infrastructure.Converters;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using AdvertisementService.ApplicationCore.Application.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NewPlace.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdvertisementController : ControllerBase
    {
        private readonly IAdvertisementApplicationService _service;

        public AdvertisementController(IAdvertisementApplicationService service)
        {
            _service = service;
        }

        // GET: api/Advertisement
        [HttpGet]
        public async Task<IEnumerable<AdvertisementRepresentation>> Get()
        {
            var ads = _service.GetAllAsync();
            List<AdvertisementDto> advertisements = new();
            await foreach (var a in ads)
                advertisements.Add(a);
            return advertisements.AsParallel().AsOrdered()
                .Select(async advertisement => await advertisement.ToRepresentation(HttpContext.Request.Path, _service))
                .AsEnumerable().Select(task => task.Result).ToList();
        }


        [HttpGet("search")]
        public async Task<List<AdvertisementRepresentation>> Search(string? estateType, string? city, double radius)
        {
            var advertisements = await _service.GetByCityAndEstateTypeAsync(city, estateType);
            return advertisements.Select(async advertisement => await advertisement.ToRepresentation(HttpContext.Request.Path, _service))
                .Select(task => task.Result).ToList();
        }

        [HttpGet("{id}")]
        public async Task<AdvertisementDetailsRepresentation> Get(int id)
        {
            return new AdvertisementDetailsRepresentation()
            {
                Resource = _service.GetById(id),
                Thumbnail = new ImageRepresentation
                {
                    Resource = await _service.GetThumbnailBase64(id),
                    MediaType = MediaTypeNames.Image.Jpeg
                }, 
                Links = new List<Link>
                {
                    new Link()
                    {
                        Rel = "self",
                        Href = HttpContext.Request.Path,
                        Method = HttpMethod.Get.ToString()
                    }
                }
            };
        }

        // POST api/Advertisement
        [Authorize]
        [HttpPost]
        public void Post(AdvertisementRepresentation advertisement)
        {
            _service.Add(advertisement.Resource.ToDomain(), advertisement.Thumbnail.Resource);
        }

        // PUT api/Advertisement/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/Advertisement/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
