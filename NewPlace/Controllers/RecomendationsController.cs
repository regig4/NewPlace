using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewPlace.ResourceRepresentations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewPlace.Controllers
{
    [Authorize]
    public class RecomendationsController : Controller
    {
        public string Test() 
        {
            return "AA";
        }
    }
}
