using FluentValidation;
using PrivateClinicsWebNet.Application.DTOs.AppointmentDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateClinicsWebNet.Application.Validation
{
    public class UpdateAppointmentsDtoValidation : AbstractValidator<UpdateAppointmentDto>
    {
        public UpdateAppointmentsDtoValidation()
        {
            RuleFor(a=>a.Id).NotNull().NotEmpty()
                .WithMessage("This field is required, so it can`t be empty");
        }
    }
}
