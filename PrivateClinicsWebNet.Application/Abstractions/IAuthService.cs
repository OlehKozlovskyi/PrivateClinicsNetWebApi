using PrivateClinicsWebNet.Application.DTOs;

namespace PrivateClinicsWebNet.Application.Abstractions
{
    public interface IAuthService
    {
        Task<string> Login(LoginDto loginDto);
        Task Register(RegisterDto registerDto);
    }
}