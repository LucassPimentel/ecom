using auth.Interfaces.Services;
using auth.Models;
using auth.Resources;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace auth.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServices _authServices;

        public AuthController(IAuthServices authServices)
        {
            _authServices = authServices;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginResource userLogin)
        {
            try
            {
                var loginResponse = await _authServices.LoginAsync(userLogin);
                var token = GenerateJwtToken(loginResponse.Email);
                return Ok(new { token });
            }
            catch (Exception)
            {
                return Unauthorized("Usuário ou senha inválidos.");
            }

        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterResource registerResource)
        {
            try
            {
                var response = await _authServices.RegisterAsync(registerResource);
                return response is not null ? CreatedAtAction(nameof(GetUserByEmail), new { email = response.Email }, response) : BadRequest("Não foi possível registrar.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("user/{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var response = await _authServices.GetUserByEmail(email);
            return response is not null ? Ok(response) : NotFound("Usuário não encontrado...");
        }

        private string GenerateJwtToken(string email)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("EC23CD44636243C1048225E5A253A8DB84EAB3F7C729C53079515E28E4E7E909"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "your_issuer_here",
                audience: "your_audience_here",
                claims: claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
