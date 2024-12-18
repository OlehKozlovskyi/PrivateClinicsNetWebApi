using Microsoft.AspNetCore.Identity;

namespace PrivateClinicsWebNet.DataAccess.Abstractions;

public interface ITokenService
{
    string GenerateJwt(IdentityUser user, string email);
}