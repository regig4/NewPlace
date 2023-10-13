using UserService.Dao;

namespace UserService.Application
{
    public class UserApplicationService
    {
        public ApplicationUser? GetByEmail(string email)
        {
            return UserDao.GetByEmail(email);
        }
    }
}
