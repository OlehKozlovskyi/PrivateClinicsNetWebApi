using PrivateClinicsWebNet.BusinessLogic.Entities;

namespace PrivateClinicsWebNet.BusinessLogic.Abstractions
{
    public interface IHasAppointments
    {
        List<Appointment> Appointments { get; set; }
    }
}