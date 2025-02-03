using FluentValidation;
using PrivateClinicsWebNet.Application.DTOs.AppointmentDTOs;
using PrivateClinicsWebNet.BusinessLogic.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
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
            RuleFor(a=>a.Date).NotEmpty()
                .Must(date => DateTime.TryParseExact(date, "yyyy-MM-dd",CultureInfo.InvariantCulture,
                DateTimeStyles.None, out DateTime parsedDate) && parsedDate > DateTime.Now.Date);
            RuleFor(a=>a.Time).NotEmpty()
                .Must(time=>DateTime.TryParseExact(time, "HH:mm:ss", CultureInfo.InvariantCulture,
                DateTimeStyles.None, out DateTime parsedTime) && parsedTime.Hour > DateTime.Now.Hour);
        }
    }
}
