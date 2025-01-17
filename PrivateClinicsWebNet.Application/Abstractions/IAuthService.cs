using PrivateClinicsWebNet.Application.DTOs;
using PrivateClinicsWebNet.Application.DTOs.AuthDTOs;

namespace PrivateClinicsWebNet.Application.Abstractions
{
    public interface IAuthService
    {
        Task<string> Login(LoginDto loginDto);
        Task Register(RegisterDto registerDto);
    }
}