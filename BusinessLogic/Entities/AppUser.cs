using Microsoft.AspNetCore.Identity;

namespace PrivateClinicsWebNet.BusinessLogic.Entities;

public class AppUser : IdentityUser
{
    public string FullName { get; set; }
}
