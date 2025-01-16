using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateClinicsWebNet.Application.DTOs
{
    public record AppointmentDto
    {
        public string ExternalId {  get; set; }
        public string PatientId { get; set; }
        public string DoctorId { get; set; }
        public DateTime Date { get; set; }
    }
}
