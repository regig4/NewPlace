using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace UserService.Controllers
{
    public class ObservedAdvertisements : ControllerBase
    {
        [HttpGet("/")]
        public ICollection<int> GetObservedAdvertisements(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
