using auth.Models;
using auth.Resources;

namespace auth.Interfaces.Repositories
{
    public interface IAuthRepository
    {
        Task<UserResource> AddUserAsync(User newUser);
        Task<User?> GetUserByEmailAsync(string email);
    }
}
