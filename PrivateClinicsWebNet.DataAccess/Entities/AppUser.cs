using Microsoft.AspNetCore.Identity;

namespace PrivateClinicsWebNet.DataAccess.Entities
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
