using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using API.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using UserService.Dao;
using UserService.Domain.Entities;

namespace API.ApplicationServices
{
    public class IdentityApplicationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly JwtSettings _jwtSettings;


        public IdentityApplicationService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
            _configuration.GetSection("Jwt").Bind(_jwtSettings);
        }

        internal async Task<AuthenticationResult> Register(string email, string password)
        {
            ApplicationUser existingUser = await _userManager.FindByEmailAsync(email);

            if (existingUser is not null)
            {
                return new AuthenticationResult
                {
                    Errors = new List<string>()
                    {
                        "User already exists"
                    }
                };
            }

            ApplicationUser newUser = new ApplicationUser
            {
                Email = email,
                UserName = email
            };

            IdentityResult createdUser = await _userManager.CreateAsync(newUser, password);

            if (!createdUser.Succeeded)
            {
                return new AuthenticationResult
                {
                    Errors = createdUser.Errors.Select(e => e.Description)
                };
            }

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            byte[] key = Encoding.ASCII.GetBytes(_jwtSettings.Key);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, email),
                    new Claim(JwtRegisteredClaimNames.Email, email),
                    new Claim("id", newUser.Id),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                }),
                Expires = DateTime.UtcNow.AddHours(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return new AuthenticationResult
            {
                Token = tokenHandler.WriteToken(token),
                Success = true
            };
        }
    }
}
