using Microsoft.EntityFrameworkCore;
using PrivateClinicsWebNet.BusinessLogic.Abstractions;
using PrivateClinicsWebNet.BusinessLogic.Entities;
using PrivateClinicsWebNet.DataAccess;
using PrivateClinicsWebNet.Infrastructure.AppointmentInfrastructure.Abstractions;
using PrivateClinicsWebNet.Infrastructure.Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public async Task<Appointment> GetAppointmentByIdAsync(string id)
        {
            var appointment = await _context.Appointments
                .FirstOrDefaultAsync(x => x.Id.ToString() == id);
            return appointment;
        }

        public async Task<bool> СreateAppointmentAsync(Appointment appointment)
        {
            var result = await _context.Appointments
                .AddAsync(appointment);
            return result.IsKeySet;
        }

        public async Task UpdateAppointmentAsync(Appointment appointment)
        {
            _context.Appointments.Update(appointment);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Appointment>> GetUserAppointmentsAsync(Expression<Func<Appointment, bool>> matchesUserId,
            int page, int pageSize)
        {
            return await _context.Appointments
                .Where(matchesUserId)
                .Skip((page-1)*pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task DeleteAppointmentAsync(string appointmentId)
        {
            var appointment = await _context.Appointments
                .FirstOrDefaultAsync(a => a.Id.ToString() == appointmentId);
            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsAppointmentExistAsync(string appointmentId)
        {
            return await _context.Appointments.AnyAsync(x => x.Id.ToString()==appointmentId);
        }
    }
}
