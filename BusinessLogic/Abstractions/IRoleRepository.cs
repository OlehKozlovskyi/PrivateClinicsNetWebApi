using Microsoft.AspNetCore.Identity;

namespace PrivateClinicsWebNet.BusinessLogic.Abstractions;

public interface IRoleRepository
{
    Task<IdentityResult> RegisterRoleAsync(string roleName);
}