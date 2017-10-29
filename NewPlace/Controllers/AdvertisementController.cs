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
        public IEnumerable<AdvertisementRepresentation> Get()
        {
            return _service.GetAll().Select(advertisement =>
                new AdvertisementRepresentation()
                {
                    Resource = advertisement,
                    Thumbnail = new ImageRepresentation()
                    {
                        Resource = _service.GetThumbnailBase64(advertisement.Id.Value),
                        MediaType = MediaTypeNames.Image.Jpeg,
                    },
                    Links = new List<Link>
                    {
                        new Link()
                        {
                            Rel = "self",
                            Href = Path.Combine("/", HttpContext.Request.Path, advertisement.Id.ToString()),
                            Method = HttpMethod.Get.ToString(),
                            Title = "Reprezentacja zasobu"
                        }
                    }
                });
        }

 
        [Route("search")]
        public IEnumerable<Representation<Advertisement>> Search(string city, string estateType, double radius)
        {
            return _service.GetByCityAndEstateType(city, estateType).Select(advertisement =>
                new Representation<Advertisement>()
                {
                    Resource = advertisement,
                    Links = new List<Link>
                    {
                        new Link()
                        {
                            Rel = "self",
                            Href = String.Join("/", HttpContext.Request.Path, advertisement.Id),
                            Method = HttpMethod.Get.ToString(),
                            Title = "Reprezentacja zasobu"
                        }
                    }
                });
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
        public void Post([FromBody]string value)
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
