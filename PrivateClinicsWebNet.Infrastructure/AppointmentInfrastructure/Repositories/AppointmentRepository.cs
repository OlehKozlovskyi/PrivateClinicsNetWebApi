using Microsoft.EntityFrameworkCore;
using PrivateClinicsWebNet.Application.DTOs;
using PrivateClinicsWebNet.BusinessLogic.Abstractions;
using PrivateClinicsWebNet.BusinessLogic.Entities;
using PrivateClinicsWebNet.DataAccess;
using PrivateClinicsWebNet.Infrastructure.Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateClinicsWebNet.Infrastructure.AppointmentInfrastructure.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ApplicationDbContext _context;

        public AppointmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<Appointment>> GetAppointmentByIdAsync(string id)
        {
            var appointment = await _context.Appointments
                .FirstAsync(x => x.ExternalId == id);
            if (appointment == null)
                return Result<Appointment>.Failure($"The appointment with ID {id} wasn`t found.");
            return await Result<Appointment>.SuccessAsync(appointment);
        }

        public async Task<Result<List<Appointment>>> GetDoctorAppointmentsAsync(string doctorId)
        {
            bool hasAppointments = await _context.Appointments
                .AnyAsync(x => x.DoctorId == doctorId);
            if (!hasAppointments)
                return Result<List<Appointment>>.Failure($"Doctor with ID {doctorId} doesn`t have any appointments yet.");
            var doctorAppointments = await _context.Appointments
                .Where(a => a.DoctorId == doctorId)
                .ToListAsync();
            return await Result<List<Appointment>>.SuccessAsync(doctorAppointments);
        }

        public async Task<Result<List<Appointment>>> GetPatientAppointmentsAsync(string patientId)
        {
            bool hasAppointments = await _context.Appointments
                .AnyAsync(x => x.PatientId == patientId);
            if (!hasAppointments)
                return Result<List<Appointment>>.Failure($"Patient with ID {patientId} doesn`t have any appointments yet.");
            var patientAppointments = await _context.Appointments
                .Where(a => a.PatientId == patientId)
                .ToListAsync();
            return await Result<List<Appointment>>.SuccessAsync(patientAppointments);
        }

        public async Task<Result<string>> СreateAppointmentAsync(Appointment appointment)
        {
            var result = await _context.Appointments
                .AddAsync(appointment);
            if (result.IsKeySet)
                return await Result<string>.SuccessAsync($"Appointment was created successfully: ID {appointment.Id}");
            return Result<string>.Failure("Failed to create the appointment due to a system issue");
        }

        public async Task<Result<string>> UpdateAppointmentAsync(Appointment appointment)
        {
            var appointmentFromDb = await _context.Appointments
                .FirstOrDefaultAsync(a => a.Id == appointment.Id);
            if (appointmentFromDb == null)
                return Result<string>.Failure();
            _context.Appointments.Update(appointment);
            await _context.SaveChangesAsync();
            return await Result<string>.SuccessAsync("Appointment was updated successfully.");
        }

        public async Task<Result<string>> DeleteAppointmentAsync(string appointmentId)
        {
            var appointment = await _context.Appointments
                .FirstOrDefaultAsync(a => a.Id.ToString() == appointmentId);
            if (appointment == null)
                return Result<string>.Failure($"Appointment with ID {appointmentId} doesn`t exist in the system");
            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();
            return await Result<string>.SuccessAsync($"Appointment has been successfully deleted.");
        }
    }
}
