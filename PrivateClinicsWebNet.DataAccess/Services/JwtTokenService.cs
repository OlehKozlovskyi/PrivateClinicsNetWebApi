using AutoMapper.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PrivateClinicsWebNet.DataAccess.Abstractions;
using PrivateClinicsWebNet.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PrivateClinicsWebNet.DataAccess.Services
{
    public class JwtTokenService : ITokenService
    {
        private readonly JwtSecurityTokenSettings _configuration;

        public JwtTokenService(IOptions<JwtSecurityTokenSettings> settings)
        {
            _configuration = settings.Value;
        }

        public string GenerateJwt(IdentityUser user, string email, IList<string> roles)
        {
            var token = GenerateEncryptedToken(GetClaimsAsync(user, email, roles), GetSigningCredentials());
            return token;
        }

        private string GenerateEncryptedToken(IEnumerable<Claim> claimsList, SigningCredentials _signingCredentials)
        {
            double jwtExpirationDays = Convert.ToDouble(_configuration.ExpirationDays);
            var token = new JwtSecurityToken(
                claims: claimsList,
                expires: DateTime.UtcNow.AddDays(jwtExpirationDays),
                signingCredentials: _signingCredentials,
                issuer: _configuration.Issuer,
                audience: _configuration.Audience);
            var tokenHandler = new JwtSecurityTokenHandler();
            string encryptedToken = tokenHandler.WriteToken(token);

            return encryptedToken;
        }

        private IEnumerable<Claim> GetClaimsAsync(IdentityUser user, string email, IList<string> roles)
        {
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, email),
            };
            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));
            return claims;
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Key));
            return new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        }
    }
}
