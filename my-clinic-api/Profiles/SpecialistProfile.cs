using AutoMapper;
using my_clinic_api.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace my_clinic_api.Profiles
{
    public class SpecialistProfile : Profile
    {
        public SpecialistProfile()
        {
            CreateMap<Specialist, DTOS.SpecialistsDto>();
             //.ForMember(x => x.department, opt => opt.Ignore())
             //.ForMember(x => x.Doctores, opt => opt.Ignore());
        }

    }
}