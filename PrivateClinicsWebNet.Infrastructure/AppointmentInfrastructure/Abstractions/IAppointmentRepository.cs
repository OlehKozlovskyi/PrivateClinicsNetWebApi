using PrivateClinicsWebNet.BusinessLogic.Entities;
using PrivateClinicsWebNet.Infrastructure.AppointmentInfrastructure.DTOs;

namespace PrivateClinicsWebNet.Infrastructure.AppointmentInfrastructure.Abstractions
{
    public interface IAppointmentRepository
    {
        Task<bool> AppointmentExistAsync(string appointmentId);
        Task<AppointmentResponseDto> GetAppointmentByIdAsync(string id);
        Task<List<AppointmentResponseDto>> GetAppointmentsAsync(string doctorId, string patientId, int page, int pageSize);
        Task<bool> TryDeleteAppointmentAsync(string appointmentId);
        Task UpdateAppointmentAsync(Appointment appointment);
        Task<bool> СreateAppointmentAsync(Appointment appointment);
    }
}