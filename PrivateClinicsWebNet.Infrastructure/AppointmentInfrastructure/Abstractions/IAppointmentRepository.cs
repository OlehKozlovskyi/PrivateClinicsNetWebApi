using PrivateClinicsWebNet.BusinessLogic.Entities;
using PrivateClinicsWebNet.Infrastructure.Shared.Wrapper;

namespace PrivateClinicsWebNet.Infrastructure.AppointmentInfrastructure.Abstractions
{
    public interface IAppointmentRepository
    {
        Task<Result<string>> DeleteAppointmentAsync(string appointmentId);
        Task<Appointment> GetAppointmentByIdAsync(string id);
        Task<Result<List<Appointment>>> GetDoctorAppointmentsAsync(string doctorId);
        Task<Result<List<Appointment>>> GetPatientAppointmentsAsync(string patientId);
        Task UpdateAppointmentAsync(Appointment appointment);
        Task<bool> СreateAppointmentAsync(Appointment appointment);
        Task<bool> IsAppointmentExistAsync(string appointmentId);
    }
}