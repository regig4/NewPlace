using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Models;
using ApplicationCore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NewPlace.ResourceRepresentations;

[AllowAnonymous]
public class AuthorizationController : Controller
{
    private readonly IAuthService _service;
    private readonly IConfiguration _configuration;

    public AuthorizationController(IAuthService service, IConfiguration configuration)
    {
        _service = service;
        _configuration = configuration;
    }

    public async Task<ActionResult<UserRepresentation>> Login([FromBody] UserRepresentation user)
    {
        var userResult = await _service.AuthorizeAsync(user.Resource, user.Password);
        if (userResult.PasswordHash != null)
        {
            user.Token = BuildToken(user.Resource);
            return user;
        }
        return Unauthorized();
    }

    private string BuildToken(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
          _configuration["Jwt:Issuer"],
          expires: DateTime.Now.AddMinutes(30),
          signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

}