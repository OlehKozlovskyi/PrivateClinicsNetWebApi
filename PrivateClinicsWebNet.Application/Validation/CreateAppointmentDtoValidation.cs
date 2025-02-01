using FluentValidation;
using PrivateClinicsWebNet.Application.DTOs.AppointmentDTOs;
using PrivateClinicsWebNet.BusinessLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateClinicsWebNet.Application.Validation
{
    public class CreateAppointmentDtoValidation : AbstractValidator<CreateAppointmentDto>
    {
        public CreateAppointmentDtoValidation() 
        {
            RuleFor(a=>a.PatientId).NotEmpty();
            RuleFor(a=>a.DoctorId).NotEmpty();
            RuleFor(a=>a.ExternalId).NotEmpty();
            RuleFor(a=>a.Date).NotEmpty();
            RuleFor(a=>a.Time).NotEmpty();
        }
    }
}
