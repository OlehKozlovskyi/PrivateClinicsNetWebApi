using AutoMapper;
using PrivateClinicsWebNet.Application.Abstractions;
using PrivateClinicsWebNet.Application.DTOs.AppointmentsDTOs;
using PrivateClinicsWebNet.BusinessLogic.Entities;
using PrivateClinicsWebNet.Infrastructure.AppointmentInfrastructure.Abstractions;
using PrivateClinicsWebNet.Infrastructure.Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PrivateClinicsWebNet.Infrastructure.AppointmentInfrastructure.Services
{
    public class AppointmentService(IAppointmentRepository appointmentRepository, 
        IMapper mapper) : IAppointmentService
    {
        public async Task<Result<AppointmentResponseDto>> GetAppointmentAsync(string id)
        {
            bool isAppointmentExist = await appointmentRepository.IsAppointmentExistAsync(id);
            
            if (!isAppointmentExist)
            {
                return Result<AppointmentResponseDto>.Failure($"The appointment with ID {id} wasn`t found.");
            }

            var appointment = await appointmentRepository.GetAppointmentByIdAsync(id);
            var response = mapper.Map<AppointmentResponseDto>(appointment);

            return await Result<AppointmentResponseDto>.SuccessAsync(response);
        }

        public async Task<Result<string>> CreateAppointmentAsync(CreateAppointmentDto appointmentDto)
        {
            var appointment = mapper.Map<Appointment>(appointmentDto);
            var isCreatedResult = await appointmentRepository.СreateAppointmentAsync(appointment);

            if (isCreatedResult)
            {
                return await Result<string>.SuccessAsync($"Appointment was created successfully: ID {appointment.Id}");
            }

            return Result<string>.Failure("Failed to create the appointment due to a system issue");
        }

        public async Task<Result<string>> UpdateAppointmentAsync(UpdateAppointmentDto appointmentDto)
        {
            var appointment = mapper.Map<Appointment>(appointmentDto);
            bool isExist = await appointmentRepository.IsAppointmentExistAsync(appointment.Id.ToString());

            if (!isExist)
            {
                return Result<string>.Failure($"The appointment with ID {appointment.Id.ToString()} cannot be update." +
                    $"It doesn`t exist in system yet");
            }
                
            await appointmentRepository.UpdateAppointmentAsync(appointment);

            return await Result<string>.SuccessAsync("Appointment was updated successfully.");
        }

        public async Task<Result<List<AppointmentResponseDto>>> GetDoctorAppointmentsAsync(string id, AppointmentsRequestDto requestDto)
        {
            Expression<Func<Appointment, bool>> matchesDoctorId = a => a.DoctorId == id;
            var appointmentsList = await appointmentRepository.GetUserAppointmentsAsync(matchesDoctorId, requestDto.Page, requestDto.PageSize);

            if (!appointmentsList.Any())
            {
                return Result<List<AppointmentResponseDto>>.Failure($"Doctor with ID {id} doesn`t have any appointments yet.");
            }

            var appointmentListResponse = mapper.Map<List<AppointmentResponseDto>>(appointmentsList);

            return await Result<List<AppointmentResponseDto>>.SuccessAsync(appointmentListResponse);
        }

        public async Task<Result<List<AppointmentResponseDto>>> GetPatientAppointmentsAsync(string id, AppointmentsRequestDto requestDto)
        {
            Expression<Func<Appointment, bool>> matchesPatientId = a => a.PatientId == id;
            var appointmentsList = await appointmentRepository.GetUserAppointmentsAsync(matchesPatientId, requestDto.Page, requestDto.PageSize);

            if (!appointmentsList.Any())
            {
                return Result<List<AppointmentResponseDto>>.Failure($"Patient with ID {id} doesn`t have any appointments yet.");
            }

            var appointmentListResponse = mapper.Map<List<AppointmentResponseDto>>(appointmentsList);

            return await Result<List<AppointmentResponseDto>>.SuccessAsync(appointmentListResponse);
        }

        public async Task<Result<string>> DeleteAppointmentAsync(string appointmentId)
        {
            bool isExist = await appointmentRepository.IsAppointmentExistAsync(appointmentId);
            
            if (!isExist)
            {
                return Result<string>.Failure($"Appointment with ID {appointmentId} doesn`t exist in the system");
            }

            await appointmentRepository.DeleteAppointmentAsync(appointmentId);
            
            return await Result<string>.SuccessAsync($"Appointment has been successfully deleted.");
        }
    }
}
