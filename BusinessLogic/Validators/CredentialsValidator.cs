using Microsoft.AspNetCore.Identity;
using PrivateClinicsWebNet.DataAccess.Exceptions;
using PrivateClinicsWebNet.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Validators
{
    public class CredentialsValidator
    {
        private readonly UserRepository _userRepository;

        public CredentialsValidator(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> IsPasswordValid(IdentityUser user, string password)
        {
            return await _userRepository.CheckPasswordAsync(user, password);
        }

        public bool IsUserRegistered(IdentityUser user)
        {
            if (user == null)
                throw new UserNotFoundException();
            return true;
        }
    }
}
