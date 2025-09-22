using auth.Interfaces.Services;
using auth.Resources;
using Microsoft.AspNetCore.Mvc;

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
                var token = _authServices.GenerateJwtToken(loginResponse);
                return Ok(new { token });
            }
            catch (ArgumentNullException)
            {
                return NotFound("Usuário não encontrado.");
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("Usuário ou senha inválidos.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro interno do servidor.");
            }

        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterResource registerResource)
        {
            try
            {
                var response = await _authServices.RegisterAsync(registerResource);
                return CreatedAtAction(nameof(GetUserByEmail), new { email = response.Email }, response);
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
    }
}
