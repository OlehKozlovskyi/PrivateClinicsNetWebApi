using PrivateClinicsWebNet.BusinessLogic.Entities;
using PrivateClinicsWebNet.Infrastructure.AppointmentInfrastructure.DTOs;
using System.Linq.Expressions;

namespace PrivateClinicsWebNet.Infrastructure.AppointmentInfrastructure.Abstractions
{
    public interface IAppointmentRepository
    {
        Task<AppointmentResponseDto> GetAppointmentByIdAsync(string id);
        Task<List<AppointmentResponseDto>> GetUserAppointmentsAsync(Expression<Func<Appointment, bool>> matchesUserId, int page, int pageSize);
        Task<bool> IsAppointmentExistAsync(string appointmentId);
        Task<bool> TryDeleteAppointmentAsync(string appointmentId);
        Task UpdateAppointmentAsync(Appointment appointment);
        Task<bool> СreateAppointmentAsync(Appointment appointment);
    }
}