using System.Threading.Tasks;
using API.ApplicationServices;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.Requests;
using UserService.Application.Responses;
using UserService.Models;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IdentityApplicationService _identityService;

        public IdentityController(IdentityApplicationService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequest request)
        {
            var result = await _identityService.Register(request.Email, request.Password);

            if (!result.Success)
            {
                return BadRequest(new AuthenticationFailureResponse
                {
                    Errors = result.Errors
                });
            }

            return Ok(new AuthenticationSuccessfulResponse
            {
                Token = result.Token
            });
        }
    }
}
