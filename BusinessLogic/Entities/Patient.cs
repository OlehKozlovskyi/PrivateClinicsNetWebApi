using Microsoft.AspNetCore.Identity;

namespace PrivateClinicsWebNet.BusinessLogic.Entities
{
    public class Patient : IdentityUser
    {
        public List<Appointment> Appointments { get; set; }
    }
}
