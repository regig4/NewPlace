using ApplicationCore.DTOs;
using ApplicationCore.Models;
using ApplicationCore.Services;
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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NewPlace.Controllers
{
    [Route("api/[controller]")]
    public class AdvertisementController : Controller
    {
        private readonly IAdvertisementService _service;

        public AdvertisementController(IAdvertisementService service)
        {
            _service = service;
        }

        // GET: api/Advertisement
        [HttpGet]
        public async Task<IEnumerable<AdvertisementRepresentation>> Get()
        {
            var advertisements = await _service.GetAllAsync();
            return advertisements.AsParallel().AsOrdered()
                .Select(async advertisement => await advertisement.ToRepresentation(HttpContext.Request.Path, _service))
                .AsEnumerable().Select(task => task.Result).ToList();
        }


        [HttpGet("search")]
        public async Task<IEnumerable<AdvertisementRepresentation>> Search(string estateType, string city, double radius)
        {
            var advertisements = await _service.GetByCityAndEstateTypeAsync(city, estateType);
            return advertisements.Select(async advertisement => await advertisement.ToRepresentation(HttpContext.Request.Path, _service))
                .Select(task => task.Result);
        }

        [HttpGet("{id}")]
        public AdvertisementDetailsRepresentation Get(int id)
        {
            return new AdvertisementDetailsRepresentation()
            {
                Resource = _service.GetById(id),
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
        [HttpPost]
        public void Post(Advertisement advertisement)
        {
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
