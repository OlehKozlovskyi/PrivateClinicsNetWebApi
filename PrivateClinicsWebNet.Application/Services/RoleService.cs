using PrivateClinicsWebNet.BusinessLogic.Repositories;
using PrivateClinicsWebNet.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PrivateClinicsWebNet.Application.Services
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
