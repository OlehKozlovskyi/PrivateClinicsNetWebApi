using Microsoft.AspNetCore.Identity;

namespace PrivateClinicsWebNet.BusinessLogic.Abstractions;

public interface IUserRepository
{
    Task AddToRoleAsync(IdentityUser user, string role);
    Task<bool> CheckPasswordAsync(IdentityUser user, string password);
    Task<IdentityUser> FindByEmailAsync(string email);
    Task<IdentityResult> RegisterUserAsync(IdentityUser user, string password);
    Task<IList<string>> GetRolesByUser(IdentityUser user);
}