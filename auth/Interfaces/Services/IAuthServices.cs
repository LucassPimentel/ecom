using auth.Models;
using auth.Resources;

namespace auth.Interfaces.Services
{
    public interface IAuthServices
    {
        Task<UserResource?> GetUserByEmail(string email);
        Task<UserResource> LoginAsync(LoginResource userLogin);
        Task<UserResource> RegisterAsync(RegisterResource registerResource);
    }
}
