using ApplicationCore.Services;
using System.Threading.Tasks;
using Common.Dto;

namespace Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        public async Task<UserDto> AuthorizeAsync(UserDto user, string password)
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
