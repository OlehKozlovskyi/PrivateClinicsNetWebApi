using AutoMapper;
using PrivateClinicsWebNet.Application.DTOs.AppointmentDTOs;
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
    public class AppointmentUpdatingProfileMap: Profile
    {
        public AppointmentUpdatingProfileMap() 
        {
            CreateMap<UpdateAppointmentDto, Appointment>()
                .ForMember(dest=>dest.Id, opt=>opt.MapFrom(src=>src.Id))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateTimeHelper.TryParseDateTime(src.Date, src.Time)));
        }
    }
}
