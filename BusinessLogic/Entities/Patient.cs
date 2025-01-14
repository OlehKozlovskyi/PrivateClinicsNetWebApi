using Microsoft.AspNetCore.Identity;
using PrivateClinicsWebNet.BusinessLogic.Abstractions;

namespace PrivateClinicsWebNet.BusinessLogic.Entities
{
    public class Patient : IdentityUser, IHasAppointments
    {
        public List<Appointment> Appointments { get; set; }
    }
}
