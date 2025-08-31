using auth.Interfaces.Repositories;
using auth.Interfaces.Services;
using auth.Models;
using auth.Resources;
using auth.Util;

namespace auth.Services
{
    public class AuthServices : IAuthServices
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _configuration;
        private readonly string _pepper;
        private readonly int _iteration = 3;

        public AuthServices(IAuthRepository authRepository, IConfiguration configuration)
        {
            _authRepository = authRepository;
            _configuration = configuration;
            _pepper = _configuration["Pepper"] ?? throw new NullReferenceException("Pepper configuration is missing.");
        }

        public async Task<UserResource?> GetUserByEmail(string email)
        {
            var user = await _authRepository.GetUserByEmailAsync(email);
            return user is not null ? new UserResource(user.Id, user.UserName, user.Email, user.CreatedAt) : null;
        }

        public async Task<UserResource> LoginAsync(LoginResource userResource)
        {
            var user = await _authRepository.GetUserByEmailAsync(userResource.Email);
            if (user is null) throw new UnauthorizedAccessException();

            var loginHashedPassword = PasswordHasher.HashPassoword(userResource.Password, user.PasswordSalt, _pepper, _iteration);
            if (loginHashedPassword != user.PasswordHash)
            {
                throw new UnauthorizedAccessException();
            }

            return new UserResource(user.Id, user.UserName, user.Email, user.CreatedAt);
        }

        public async Task<UserResource> RegisterAsync(RegisterResource registerResource)
        {
            var user = await _authRepository.GetUserByEmailAsync(registerResource.Email);
            if (user is not null)
            {
                throw new InvalidOperationException("Já existe uma conta com esse e-mail.");
            }

            ValidatePassword.Validate(registerResource.Password);

            var newUser = User.Create(registerResource.Email, registerResource.UserName, registerResource.Password, _pepper, _iteration);
            return await _authRepository.AddUserAsync(newUser);
        }
    }
}
