using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PrivateClinicsWebNet.Application.DTOs;
using PrivateClinicsWebNet.Application.Exceptions;
using PrivateClinicsWebNet.BusinessLogic.Repositories;
using PrivateClinicsWebNet.DataAccess.Abstractions;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PrivateClinicsWebNet.Application.Services
{
    public class AuthService
    {
        private readonly UserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public AuthService(UserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<string> Login(LoginDto loginDto)
        {
            var user = await _userRepository.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                throw new UserNotFoundException();
            }
            var passwordValid = await _userRepository.CheckPasswordAsync(user, loginDto.Password);
            if (!passwordValid)
            {
                throw new InvalidPasswordException();
            }

            var token = _tokenService.GenerateJwt(user, loginDto.Email);
            return token;
        }

        public async Task Register(RegisterDto registerDto)
        {
            var user = new IdentityUser { UserName = registerDto.Email, Email = registerDto.Email };
            var result = await _userRepository.RegisterUser(user, registerDto.Password);
            if (!result.Succeeded)
            {
                throw new RegistrationFailedException();
            }
            await _userRepository.AddToRoleAsync(user, registerDto.UserRole);
        }
    }
}
