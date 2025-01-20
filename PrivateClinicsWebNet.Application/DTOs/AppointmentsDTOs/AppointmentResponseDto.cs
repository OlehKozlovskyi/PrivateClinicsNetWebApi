using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateClinicsWebNet.Application.DTOs.AppointmentsDTOs
{
    public record AppointmentResponseDto
    {
        public string AppointmentId {  get; init; }
        public string PatientName {  get; init; }
        public string DoctorName {  get; init; }
        public string DoctorType {  get; init; }
        public DateTime Date { get; init; }
    }
}
