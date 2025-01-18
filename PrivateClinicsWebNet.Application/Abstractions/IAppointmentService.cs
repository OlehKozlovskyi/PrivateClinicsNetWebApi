using PrivateClinicsWebNet.Application.DTOs.AppointmentsDTOs;
using PrivateClinicsWebNet.Infrastructure.Shared.Wrapper;

namespace PrivateClinicsWebNet.Application.Abstractions
{
    public interface IAppointmentService
    {
        Task<Result<string>> CreateAppointmentAsync(CreateAppointmentDto appointmentDto);
        Task<Result<string>> DeleteAppointmentAsync(string appointmentId);
        Task<Result<AppointmentResponseDto>> GetAppointmentAsync(string id);
        Task<Result<List<AppointmentResponseDto>>> GetDoctorAppointmentsAsync(string id, AppointmentsRequestDto requestDto);
        Task<Result<List<AppointmentResponseDto>>> GetPatientAppointmentsAsync(string id, AppointmentsRequestDto requestDto);
        Task<Result<string>> UpdateAppointmentAsync(UpdateAppointmentDto appointmentDto);
    }
}