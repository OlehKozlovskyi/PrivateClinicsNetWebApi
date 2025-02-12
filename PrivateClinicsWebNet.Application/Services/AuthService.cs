using PrivateClinicsWebNet.Application.Abstractions;
using PrivateClinicsWebNet.Application.DTOs;
using PrivateClinicsWebNet.Application.DTOs.AuthDTOs;
using PrivateClinicsWebNet.Application.Exceptions;
using PrivateClinicsWebNet.BusinessLogic.Abstractions;
using PrivateClinicsWebNet.DataAccess.Abstractions;
using PrivateClinicsWebNet.Infrastructure.Shared.Wrapper;

namespace PrivateClinicsWebNet.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IUserFactory _userFactory;

        public AuthService(IUserRepository userRepository, ITokenService tokenService, IUserFactory userFactory)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _userFactory = userFactory;
        }

        public async Task<Result<string>> Login(LoginDto loginDto)
        {
            var user = await _userRepository.FindByEmailAsync(loginDto.Email);

            if (user is null || user.UserName != loginDto.Email)
            {
                return Result<string>.Failure("User not found.");
            }

            var passwordValid = await _userRepository.CheckPasswordAsync(user, loginDto.Password);
            
            if (!passwordValid)
            {
                throw new InvalidPasswordException();
            }

            var userRoles = await _userRepository.GetRolesByUser(user);
            var token = _tokenService.GenerateJwt(user, loginDto.Email, userRoles);
            return await Result<string>.SuccessAsync(token);
        }

        public async Task<Result<string>> Register(RegisterDto registerDto)
        {
            var user = _userFactory.GetUser(registerDto.Email, registerDto.UserRole);
            var result = await _userRepository.RegisterUserAsync(user, registerDto.Password);
            
            if (!result.Succeeded)
            {
                return Result<string>.Failure("Registration failed");
            }

            await _userRepository.AddToRoleAsync(user, registerDto.UserRole);
            return await Result<string>.SuccessAsync("User was registered successfully");
        }
    }
}
