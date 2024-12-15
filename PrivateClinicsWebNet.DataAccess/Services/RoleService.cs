using PrivateClinicsWebNet.DataAccess.Repositories;
using PrivateClinicsWebNet.DataAccess.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PrivateClinicsWebNet.DataAccess.Services
{
    public class RoleService
    {
        private readonly RoleRepository _roleRepository;

        public RoleService(RoleRepository roleRepository) 
        {
            _roleRepository = roleRepository;
        }

        public async Task CreateRoleAsync(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
                throw new ArgumentNullException(nameof(roleName));
            var result = await _roleRepository.RegisterRoleAsync(roleName);
            if (!result.Succeeded)
                throw new FailedCreateRoleOperationException();
        }
    }
}
