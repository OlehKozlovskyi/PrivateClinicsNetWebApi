using AutoMapper;
using FluentValidation;
using PrivateClinicsWebNet.Application.Abstractions;
using PrivateClinicsWebNet.Application.DTOs.AppointmentDTOs;
using PrivateClinicsWebNet.Application.Exceptions;
using PrivateClinicsWebNet.BusinessLogic.Entities;
using PrivateClinicsWebNet.Infrastructure.AppointmentInfrastructure.Abstractions;
using PrivateClinicsWebNet.Infrastructure.AppointmentInfrastructure.DTOs;
using PrivateClinicsWebNet.Infrastructure.Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PrivateClinicsWebNet.Application.Services
{
    public class AppointmentService(IAppointmentRepository appointmentRepository,
        IMapper mapper,
        IValidator<AppointmentsRequestDto> requestValidator,
        IValidator<CreateAppointmentDto> createDtoValidator,
        IValidator<UpdateAppointmentDto> updateDtoValidator) : IAppointmentService
    {
        public async Task<Result<AppointmentResponseDto>> GetAppointmentAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new InvalidAppointmentsIdException();
            }

            bool isAppointmentExist = await appointmentRepository.IsAppointmentExistAsync(id);

            if (!isAppointmentExist)
            {
                return Result<AppointmentResponseDto>.Failure($"The appointment with ID {id} wasn`t found.");
            }

            var response = await appointmentRepository.GetAppointmentByIdAsync(id);

            return await Result<AppointmentResponseDto>.SuccessAsync(response);
        }

        public async Task<Result<string>> CreateAppointmentAsync(CreateAppointmentDto appointmentDto)
        {
            var validationResult = createDtoValidator.Validate(appointmentDto);
            
            if (!validationResult.IsValid)
            {
                throw new InvalidAppointmentDataException();
            }

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
            var validationResult = updateDtoValidator.Validate(appointmentDto);
            
            if (!validationResult.IsValid)
            {
                throw new InvalidAppointmentDataException();
            }

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

        public async Task<Result<List<AppointmentResponseDto>>> GetUserAppointmentsAsync(string id, string userType, AppointmentsRequestDto requestDto)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new InvalidUserIdException($"Invalid user ID: {id}");
            }

            var validationResult = requestValidator.Validate(requestDto);
            
            if (!validationResult.IsValid)
            {
                return Result<List<AppointmentResponseDto>>.Failure($"Page and PageSize must be greater than 0");
            }

            var appointmentListResponse = await appointmentRepository.GetUserAppointmentsWithPaginationAsync(id, userType, requestDto.Page, requestDto.PageSize);

            if (!appointmentListResponse.Any())
            {
                return Result<List<AppointmentResponseDto>>.Failure($"User with ID {id} doesn`t have any appointments yet.");
            }

            return await Result<List<AppointmentResponseDto>>.SuccessAsync(appointmentListResponse);
        }

        public async Task<Result<string>> DeleteAppointmentAsync(string appointmentId)
        {
            if (string.IsNullOrEmpty(appointmentId))
            {
                throw new InvalidAppointmentsIdException();
            }

            bool isCompleted = await appointmentRepository.TryDeleteAppointmentAsync(appointmentId);

            if (!isCompleted)
            {
                return Result<string>.Failure($"Failed to delete the appointment.");
            }

            return await Result<string>.SuccessAsync($"Appointment has been successfully deleted.");
        }
    }
}
