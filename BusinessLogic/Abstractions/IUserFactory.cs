using Microsoft.AspNetCore.Identity;

namespace PrivateClinicsWebNet.BusinessLogic.Abstractions
{
    public interface IUserFactory
    {
        IdentityUser GetUser(string email, string identityRole);
    }
}