using PrivateClinicsWebNet.BusinessLogic.Entities;
using PrivateClinicsWebNet.Infrastructure.AppointmentInfrastructure.Abstractions;
using PrivateClinicsWebNet.Infrastructure.Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateClinicsWebNet.Infrastructure.AppointmentInfrastructure.Services
{
    public class AppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository) 
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<Result<Appointment>> GetAppointmentAsync(string id)
        {
            bool isAppointmentExist = await _appointmentRepository.IsAppointmentExistAsync(id);
            if (!isAppointmentExist)
                return Result<Appointment>.Failure($"The appointment with ID {id} wasn`t found.");
            var appointment = await _appointmentRepository.GetAppointmentByIdAsync(id);
            return await Result<Appointment>.SuccessAsync(appointment);
        }

        public async Task<Result<string>> CreateAppointmentAsync(Appointment appointment)
        {
            var isCreatedResult = await _appointmentRepository.СreateAppointmentAsync(appointment);
            if(isCreatedResult)
                return await Result<string>.SuccessAsync($"Appointment was created successfully: ID {appointment.Id}");
            return Result<string>.Failure("Failed to create the appointment due to a system issue");
        }

        public async Task<Result<string>> UpdateAppointmentAsync(Appointment appointment)
        {
            bool isExist = await _appointmentRepository.IsAppointmentExistAsync(appointment.Id.ToString());
            if(!isExist)
                return Result<string>.Failure($"The appointment with ID {appointment.Id.ToString()} cannot be update." +
                    $"It doesn`t exist in system yet");
            await _appointmentRepository.UpdateAppointmentAsync(appointment);
            return await Result<string>.SuccessAsync("Appointment was updated successfully.");
        }
    }
}
