using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PrivateClinicsWebNet.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateClinicsWebNet.Application.Mapping
{
    public class UserRegistrationProfileMap : Profile
    {
        public UserRegistrationProfileMap() 
        {
            CreateMap<RegisterDto, IdentityUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src=>src.Email.ToLower()));
        }
    }
}
