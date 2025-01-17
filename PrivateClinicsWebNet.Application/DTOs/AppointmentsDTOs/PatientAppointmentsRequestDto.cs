using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateClinicsWebNet.Application.DTOs.AppointmentsDTOs
{
    public record PatientAppointmentsRequestDto
    {
        public required string PatientId {  get; set; }
        public int Page {  get; set; }
        public int PageSize { get; set; }
    }
}
