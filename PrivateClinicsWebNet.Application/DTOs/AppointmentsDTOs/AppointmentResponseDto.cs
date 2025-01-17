using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateClinicsWebNet.Application.DTOs.AppointmentsDTOs
{
    public record AppointmentResponseDto
    {
        public string AppointmentId {  get; set; }
        public string PatientName {  get; set; }
        public string DoctorName {  get; set; }
        public string DoctorType {  get; set; }
        public DateTime Date { get; set; }
    }
}
