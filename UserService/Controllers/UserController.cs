using Microsoft.AspNetCore.Mvc;
using UserService.Application;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserApplicationService _userService;

        public UserController(UserApplicationService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetByEmail([FromQuery] string email)
        {
            Dao.ApplicationUser? result = _userService.GetByEmail(email);

            if (result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
