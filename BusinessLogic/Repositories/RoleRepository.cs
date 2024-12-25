using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrivateClinicsWebNet.BusinessLogic.Exceptions;
using PrivateClinicsWebNet.BusinessLogic.Abstractions;

namespace PrivateClinicsWebNet.BusinessLogic.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public RoleRepository(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<IdentityResult> RegisterRoleAsync(string roleName)
    {
        if (await _roleManager.FindByNameAsync(roleName) != null)
            throw new RoleAlreadyExistException(roleName);
        return await _roleManager.CreateAsync(new IdentityRole(roleName));
    }
}
