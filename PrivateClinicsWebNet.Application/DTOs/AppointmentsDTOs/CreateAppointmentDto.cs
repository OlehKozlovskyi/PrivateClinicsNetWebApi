using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateClinicsWebNet.Application.DTOs.AppointmentsDTOs
{
    public record CreateAppointmentDto
    {
        public string ExternalId { get; set; }
        public string PatientId { get; set; }
        public string DoctorId { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
    }
}
