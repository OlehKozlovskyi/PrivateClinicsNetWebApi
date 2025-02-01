using FluentValidation;
using PrivateClinicsWebNet.Application.DTOs.AppointmentDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateClinicsWebNet.Application.Validation
{
    public class AppointmentsRequestDtoValidator : AbstractValidator<AppointmentsRequestDto>
    {
        public AppointmentsRequestDtoValidator()
        {
            RuleFor(request => request.Page).GreaterThan(0);
            RuleFor(request=>request.PageSize).GreaterThan(0);
        }
    }
}
