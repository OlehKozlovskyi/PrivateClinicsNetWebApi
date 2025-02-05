using AutoMapper;
using PrivateClinicsWebNet.Application.DTOs.AppointmentDTOs;
using PrivateClinicsWebNet.Application.Exceptions;
using PrivateClinicsWebNet.Application.Helpers;
using PrivateClinicsWebNet.BusinessLogic.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateClinicsWebNet.Application.Mapping
{
    public class AppointmentCreationProfileMap : Profile
    {
        public AppointmentCreationProfileMap() 
        {
            CreateMap<CreateAppointmentDto, Appointment>()
                .ForMember(dest => dest.PatientId, opt => opt.MapFrom(src => src.PatientId))
                .ForMember(dest => dest.DoctorId, opt => opt.MapFrom(src => src.DoctorId))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateTimeHelper.TryParseDateTime(src.Date, src.Time)));
        }
    }
}
