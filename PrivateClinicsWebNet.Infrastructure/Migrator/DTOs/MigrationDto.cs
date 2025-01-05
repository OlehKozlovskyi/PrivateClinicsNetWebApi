using PrivateClinicsWebNet.BusinessLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateClinicsWebNet.Infrastructure.Migrator.DTOs
{
    public record MigrationDto
    {
        public Doctor Doctor { get; set; }
        public List<Patient> PatientsList { get; set; }
    }
}
