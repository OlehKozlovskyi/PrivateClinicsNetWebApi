using Microsoft.AspNetCore.Mvc;
using PrivateClinicsWebNet.DataAccess;
using PrivateClinicsWebNet.Application.DTOs;

namespace PrivateClinicsNetWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthConroller : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthConroller(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            try
            {
                await _authService.Register(model.Email, model.Password, model.UserRole);
                return Ok("User registered successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            try
            {
                string token = await _authService.Login(model.Email, model.Password);
                return Ok(new { Token = token});
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
