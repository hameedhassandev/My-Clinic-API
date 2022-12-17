using AutoMapper;
using my_clinic_api.Models;

namespace my_clinic_api.Profiles
{
    public class PatientProfile : Profile
    {
        public PatientProfile()
        {
            CreateMap<Patient, DTOS.PatientDto>()
                .ForMember(d => d.Id, a => a.MapFrom(s => s.Id));

            CreateMap<ApplicationUser, DTOS.PatientDto>()
                .ForMember(d => d.Id, a => a.MapFrom(s => s.Id));
        }
    }
}
