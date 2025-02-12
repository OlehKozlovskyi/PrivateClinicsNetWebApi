using PrivateClinicsWebNet.Application.DTOs;
using PrivateClinicsWebNet.Application.DTOs.AuthDTOs;
using PrivateClinicsWebNet.Infrastructure.Shared.Wrapper;

namespace PrivateClinicsWebNet.Application.Abstractions
{
    public interface IAuthService
    {
        Task<Result<string>> Login(LoginDto loginDto);
        Task<Result<string>> Register(RegisterDto registerDto);
    }
}