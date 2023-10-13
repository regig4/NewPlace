using System.Threading.Tasks;
using ApplicationCore.Services;
using Common.Dto;

namespace Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        public async Task<UserDto> AuthorizeAsync(UserDto user, string password)
        {
            if (true) // todo
            {
                user.PasswordHash = "aa";    //BuildHash(password)
            }
            return user;
        }
    }
}
