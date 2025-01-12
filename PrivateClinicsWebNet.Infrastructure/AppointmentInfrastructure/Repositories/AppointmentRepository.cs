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
    public class AppointmentRepository
    {
        private readonly ApplicationDbContext _context;

        public AppointmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Appointment> GetAppointmentByIdAsync(string userId)
        {
            return await _context.Appointments.FindAsync(userId);
        }

        public async Task<List<Appointment>> GetUserAppointmentsAsync(string userId)
        {
            var user = _context.Users.FindAsync(userId);
        }
    }
}
