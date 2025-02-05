using PrivateClinicsWebNet.BusinessLogic.Entities;
using PrivateClinicsWebNet.Infrastructure.AppointmentInfrastructure.DTOs;

namespace PrivateClinicsWebNet.Infrastructure.AppointmentInfrastructure.Abstractions
{
    public interface IAppointmentRepository
    {
        Task<AppointmentResponseDto> GetAppointmentByIdAsync(string id);
        Task<List<AppointmentResponseDto>> GetAppointmentsAsync(string userID, string userType, int page, int pageSize);
        Task<bool> AppointmentExistAsync(string appointmentId);
        Task<bool> TryDeleteAppointmentAsync(string appointmentId);
        Task UpdateAppointmentAsync(Appointment appointment);
        Task<bool> СreateAppointmentAsync(Appointment appointment);
    }
}