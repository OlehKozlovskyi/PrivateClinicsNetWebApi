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
            var type = UserRegistry.GetUserTypeByName(identityRole);
            var userInstance = (IdentityUser)Activator.CreateInstance(type);
            userInstance.Email = email;
            userInstance.UserName = email;
            return userInstance;
        }
    }
}
