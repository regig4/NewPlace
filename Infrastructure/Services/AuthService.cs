using ApplicationCore.Models;
using ApplicationCore.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        public async Task<User> AuthorizeAsync(User user, string password)
        {
            string token = null;
            if(true) // todo
            {
                user.PasswordHash = "aa";    //BuildHash(password)
            }
            return user;
        }
    }
}
