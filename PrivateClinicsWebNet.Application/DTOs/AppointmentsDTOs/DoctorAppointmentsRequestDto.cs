using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateClinicsWebNet.Application.DTOs.AppointmentsDTOs
{
    public record DoctorAppointmentsRequestDto
    {
        public required string DoctorId {  get; set; }
        public int Page {  get; set; }
        public int PageSize { get; set; }
    }
}
