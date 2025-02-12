using Microsoft.EntityFrameworkCore;
using PrivateClinicsWebNet.BusinessLogic.Abstractions;
using PrivateClinicsWebNet.BusinessLogic.Entities;
using PrivateClinicsWebNet.DataAccess;
using PrivateClinicsWebNet.Infrastructure.AppointmentInfrastructure.Abstractions;
using PrivateClinicsWebNet.Infrastructure.AppointmentInfrastructure.DTOs;
using PrivateClinicsWebNet.Infrastructure.AppointmentInfrastructure.Exceptions;
using PrivateClinicsWebNet.Infrastructure.AppointmentInfrastructure.Specifications;
using PrivateClinicsWebNet.Infrastructure.Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PrivateClinicsWebNet.Infrastructure.AppointmentInfrastructure.Repositories
{
    public class AppointmentRepository(ApplicationDbContext _context) : IAppointmentRepository
    {
        private enum AppointmentsSupportEntities
        {
            Doctor,
            Patient
        }

        public async Task<AppointmentResponseDto> GetAppointmentByIdAsync(string id)
        {
            var response = await _context.Appointments
                .Where(x => x.Id.ToString() == id)
                .Select(e => new AppointmentResponseDto
                {
                    AppointmentId = e.Id.ToString(),
                    PatientName = e.Patient.UserName,
                    DoctorName = e.Doctor.UserName,
                    DoctorType = e.Doctor.DoctorType,
                    Date = e.Date,
                })
                .FirstOrDefaultAsync();
            return response;
        }

        public async Task<bool> СreateAppointmentAsync(Appointment appointment)
        {
            appointment.Id = Guid.NewGuid();
            var result = await _context.Appointments
                .AddAsync(appointment);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task UpdateAppointmentAsync(Appointment appointment)
        {
            var existingAppointment = await _context.Appointments.FindAsync(appointment.Id);
            if (appointment.Date != null)
                existingAppointment.Date = appointment.Date;
            await _context.SaveChangesAsync();
        }

        public async Task<List<AppointmentResponseDto>> GetAppointmentsAsync(string doctorId, string patientId, int page, int pageSize)
        {
            var appointmentSpec = new AppointmentFilterSpecification(doctorId, patientId);
            return await _context.Appointments
                .Where(appointmentSpec.IsSatisfiedBy())
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(entity => new AppointmentResponseDto
                {
                    AppointmentId = entity.Id.ToString(),
                    PatientName = entity.Patient.UserName,
                    DoctorName = entity.Doctor.UserName,
                    DoctorType = entity.Doctor.DoctorType,
                    Date = entity.Date,
                })
                .ToListAsync();
        }

        public async Task<bool> TryDeleteAppointmentAsync(string appointmentId)
        {
            var rowsAffected = await _context.Appointments
                .Where(x => x.Id.ToString() == appointmentId)
                .ExecuteDeleteAsync();
            return rowsAffected > 0;
        }

        public async Task<bool> AppointmentExistAsync(string appointmentId)
        {
            return await _context.Appointments.AnyAsync(x => x.Id.ToString() == appointmentId);
        }
    }
}
