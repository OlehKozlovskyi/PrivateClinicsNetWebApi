using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateClinicsWebNet.Application.DTOs.AppointmentsDTOs
{
    public record AppointmentsRequestDto
    {
        public int Page {  get; init; }
        public int PageSize { get; init; }
    }
}
