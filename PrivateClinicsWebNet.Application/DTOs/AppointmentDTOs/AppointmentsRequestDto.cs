using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateClinicsWebNet.Application.DTOs.AppointmentDTOs
{
    public record AppointmentsRequestDto
    {
        public int Page { get; init; }
        public int PageSize { get; init; }
    }
}
