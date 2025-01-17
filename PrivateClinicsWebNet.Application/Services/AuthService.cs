using PrivateClinicsWebNet.Application.Abstractions;
using PrivateClinicsWebNet.Application.DTOs;
using PrivateClinicsWebNet.Application.DTOs.AuthDTOs;
using PrivateClinicsWebNet.Application.Exceptions;
using PrivateClinicsWebNet.BusinessLogic.Abstractions;
using PrivateClinicsWebNet.DataAccess.Abstractions;

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

        public async Task<string> Login(LoginDto loginDto)
        {
            var user = await _userRepository.FindByEmailAsync(loginDto.Email);
            if (user.UserName != loginDto.Email)
            {
                throw new UserNotFoundException();
            }
            var passwordValid = await _userRepository.CheckPasswordAsync(user, loginDto.Password);
            if (!passwordValid)
            {
                throw new InvalidPasswordException();
            }
            var userRoles = await _userRepository.GetRolesByUser(user);
            var token = _tokenService.GenerateJwt(user, loginDto.Email, userRoles);
            return token;
        }

        public async Task Register(RegisterDto registerDto)
        {
            var user = _userFactory.GetUser(registerDto.Email, registerDto.UserRole);
            var result = await _userRepository.RegisterUserAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                throw new RegistrationFailedException();
            }
            await _userRepository.AddToRoleAsync(user, registerDto.UserRole);
        }
    }
}
