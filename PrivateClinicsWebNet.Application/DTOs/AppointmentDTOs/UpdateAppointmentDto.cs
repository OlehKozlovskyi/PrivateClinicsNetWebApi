using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateClinicsWebNet.Application.DTOs.AppointmentDTOs
{
    public record UpdateAppointmentDto
    {
        public string Id { get; init; }
        public string? Date { get; init; }
        public string? Time { get; init; }
    }
}
