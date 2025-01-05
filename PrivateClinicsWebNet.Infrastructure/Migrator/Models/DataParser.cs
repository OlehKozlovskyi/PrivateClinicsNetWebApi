using PrivateClinicsWebNet.BusinessLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateClinicsWebNet.Infrastructure.Migrator.Models
{
    public class DataParser
    {
        public List<Patient> GetPatients(Doctor doctor)
        {
            return doctor.Appointments
                .Select(x => x.Patient)
                .ToLookup(x => x.Id)
                .Select(x => x.First())
                .ToList();
        }

        public Doctor GetDoctor(Doctor doctor)
        {
            foreach (var appointment in doctor.Appointments)
                appointment.Patient = null;
            return doctor;
        }
    }
}
