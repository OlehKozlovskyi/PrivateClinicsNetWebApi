using Microsoft.AspNetCore.Identity;
using PrivateClinicsWebNet.BusinessLogic.Abstractions;
using PrivateClinicsWebNet.BusinessLogic.Entities;
using PrivateClinicsWebNet.BusinessLogic.Exceptions;

namespace PrivateClinicsWebNet.BusinessLogic.Factories
{
    public class UserFactory : IUserFactory
    {
        public IdentityUser GetUser(string email, string identityRole)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(identityRole))
                throw new NullReferenceException();
            switch (identityRole)
            {
                case "Doctor":
                    return new Doctor() { Email = email, UserName = email };
                case "Patient":
                    return new Patient() { Email = email, UserName = email };
                case "Admin":
                    return new Admin() { Email = email, UserName = email };
                default:
                    throw new InvalidUserRoleException(identityRole);
            }
        }
    }
}
