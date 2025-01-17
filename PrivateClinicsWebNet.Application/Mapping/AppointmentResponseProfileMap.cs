using AutoMapper;
using PrivateClinicsWebNet.Application.DTOs.AppointmentsDTOs;
using PrivateClinicsWebNet.BusinessLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateClinicsWebNet.Application.Mapping
{
    public class AppointmentResponseProfileMap:Profile
    {
        public AppointmentResponseProfileMap() 
        {
            CreateMap<Appointment, AppointmentResponseDto>()
                .ForMember(dest => dest.AppointmentId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => src.Patient.UserName))
                .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => src.Doctor.UserName))
                .ForMember(dest => dest.DoctorType, opt => opt.MapFrom(src => src.Doctor.DoctorType))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date));

        }
    }
}
