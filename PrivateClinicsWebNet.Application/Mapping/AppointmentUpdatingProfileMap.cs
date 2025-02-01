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
    public class AppointmentUpdatingProfileMap: Profile
    {
        public AppointmentUpdatingProfileMap() 
        {
            CreateMap<UpdateAppointmentDto, Appointment>()
                .ForMember(dest=>dest.Id, opt=>opt.MapFrom(src=>src.Id))
                .ForMember(dest=>dest.PatientId, opt=>opt.MapFrom(src=>src.PatientId))
                .ForMember(dest=>dest.DoctorId, opt=>opt.MapFrom(src=>src.DoctorId))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => TryParseDate(src.Date, src.Time)));
        }

        private static DateTime TryParseDate(string date, string time)
        {
            DateTime parsedDateTime;
            if(DateTime.TryParse($"{date} {time}", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDateTime))
                return parsedDateTime;
            return
                DateTime.MinValue;
        }
    }
}
