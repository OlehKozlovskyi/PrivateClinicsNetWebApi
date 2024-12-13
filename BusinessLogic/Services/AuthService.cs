using BusinessLogic.Validators;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PrivateClinicsWebNet.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class AuthService
    {
        private readonly UserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly CredentialsValidator _credentialsValidator;

        public AuthService(UserRepository userRepository, IConfiguration configuration, 
            CredentialsValidator credentialsValidator)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _credentialsValidator = credentialsValidator;
        }

        public async Task<string> Login(string email, string password)
        {
            var user = await _userRepository.FindByEmailAsync(email);
            bool isUserRegisteredFlag = _credentialsValidator.IsUserRegistered(user);
            bool isPasswordValidFlag = await _credentialsValidator.IsPasswordValid(user, password);
            if ( isUserRegisteredFlag && isPasswordValidFlag)
            {
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                    new Claim(JwtRegisteredClaimNames.Email, email)
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddDays(30),
                    signingCredentials: creds);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
        }



        //private string GenerateEncryptedToken()


    }
}
