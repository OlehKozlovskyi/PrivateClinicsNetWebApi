using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PrivateClinicsWebNet.DataAccess.Exceptions;
using PrivateClinicsWebNet.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PrivateClinicsWebNet.DataAccess
{
    public class AuthService
    {
        private readonly UserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthService(UserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<string> Login(string email, string password)
        {
           var user = await _userRepository.FindByEmailAsync(email);
            if (user == null)
            {
                throw new UserNotFoundException();
            }
            var passwordValid = await _userRepository.CheckPasswordAsync(user, password);
            if (!passwordValid)
            {
                throw new InvalidPasswordException();
            }

            var token = GenerateJwt(user, email);
            return token;
        }

        public async Task Register(string email, string password, string role)
        {
            var user = new IdentityUser { UserName = email, Email = email };
            var result = await _userRepository.RegisterUser(user, password);
            if (!result.Succeeded)
            {
                throw new RegistrationFailedException();
            }
            await _userRepository.AddToRoleAsync(user, role);
        }

        private string GenerateJwt(IdentityUser user, string email)
        {
            var token = GenerateEncryptedToken(GetClaimsAsync(user, email), GetSigningCredentials());
            return token;
        }

        private string GenerateEncryptedToken(IEnumerable<Claim> claimsList, SigningCredentials signingCredentials)
        {
            var token = new JwtSecurityToken(
                claims: claimsList,
                expires: DateTime.UtcNow.AddDays(14),
                signingCredentials: signingCredentials);
            var tokenHandler = new JwtSecurityTokenHandler();
            string encryptedToken = tokenHandler.WriteToken(token);
            return encryptedToken;
        }

        private IEnumerable<Claim> GetClaimsAsync(IdentityUser user, string email)
        {
            var claims = new List<Claim>() 
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, email)
            };
            return claims;
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            return new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        }
    }
}
