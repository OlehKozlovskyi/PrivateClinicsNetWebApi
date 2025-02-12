using Microsoft.AspNetCore.Mvc;
using PrivateClinicsNetWebApi.Extensions;
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
           var result = await _authService.Register(model);
           return result.ToResponse();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var result = await _authService.Login(model);
            return result.ToResponse();
        }
    }
}
