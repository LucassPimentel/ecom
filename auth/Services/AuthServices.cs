using auth.Interfaces.Repositories;
using auth.Interfaces.Services;
using auth.Models;
using auth.Resources;
using auth.Util;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
            _pepper = _configuration["Jwt:Pepper"] ?? throw new NullReferenceException("Pepper configuration is missing.");
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

        public string GenerateJwtToken(string email)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var cryptoKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(cryptoKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
