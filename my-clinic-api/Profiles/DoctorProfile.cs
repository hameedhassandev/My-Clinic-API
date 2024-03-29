﻿using AutoMapper;
using my_clinic_api.Models;
using System.Numerics;

namespace my_clinic_api.Profiles
{
    public class DoctorProfile : Profile
    {
        public DoctorProfile()
        {

            CreateMap<Doctor, DTOS.DoctorDto>()
                .ForMember(d => d.Specialists , a => a.MapFrom(d => d.Specialists));
            CreateMap<ApplicationUser, DTOS.DoctorDto>()
                    .ForMember(d => d.Id, a => a.MapFrom(s => s.Id));
        }
    }

}
