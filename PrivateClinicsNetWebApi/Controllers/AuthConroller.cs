using Microsoft.AspNetCore.Mvc;
using PrivateClinicsWebNet.Application.DTOs;
using PrivateClinicsWebNet.Application.Services;

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
            await _authService.Register(model);
            return Ok("User registered successfully");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            string token = await _authService.Login(model);
            return Ok(new { Token = token});
        }
    }
}
