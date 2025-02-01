using Microsoft.AspNetCore.Mvc;
using PrivateClinicsWebNet.Application.Abstractions;
using PrivateClinicsWebNet.Application.DTOs;
using PrivateClinicsWebNet.Application.DTOs.AuthDTOs;
using PrivateClinicsWebNet.Application.Services;

namespace PrivateClinicsNetWebApi.Controllers
{
    [ApiController]
    [Route("api/v1/authentication")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
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
