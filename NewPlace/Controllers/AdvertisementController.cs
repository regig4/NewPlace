using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Models;
using ApplicationCore.Services;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

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

        // GET: api/Advertisement/5
        [HttpGet("{id}")]
        public Advertisement Get(int id)
        {
            return _service.GetById(id);
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
