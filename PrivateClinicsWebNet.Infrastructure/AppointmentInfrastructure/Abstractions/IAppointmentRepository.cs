using PrivateClinicsWebNet.BusinessLogic.Entities;
using PrivateClinicsWebNet.Infrastructure.Shared.Wrapper;
using System.Linq.Expressions;

namespace PrivateClinicsWebNet.Infrastructure.AppointmentInfrastructure.Abstractions
{
    public interface IAppointmentRepository
    {
        Task DeleteAppointmentAsync(string appointmentId);
        Task<Appointment> GetAppointmentByIdAsync(string id);
        Task<List<Appointment>> GetUserAppointmentsAsync(Expression<Func<Appointment, bool>> matchesUserId);
        Task UpdateAppointmentAsync(Appointment appointment);
        Task<bool> СreateAppointmentAsync(Appointment appointment);
        Task<bool> IsAppointmentExistAsync(string appointmentId);
    }
}