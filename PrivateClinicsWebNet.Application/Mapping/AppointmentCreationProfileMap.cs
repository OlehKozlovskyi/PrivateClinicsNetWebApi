using AutoMapper;
using PrivateClinicsWebNet.Application.DTOs.AppointmentDTOs;
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
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.ExternalId))
                .ForMember(dest => dest.PatientId, opt => opt.MapFrom(src => src.PatientId))
                .ForMember(dest => dest.DoctorId, opt => opt.MapFrom(src => src.DoctorId))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateTime.Parse($"{src.Date} {src.Time}", CultureInfo.InvariantCulture)));
        }
    }
}
