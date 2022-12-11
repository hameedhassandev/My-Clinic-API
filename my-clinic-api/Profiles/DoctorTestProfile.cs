using my_clinic_api.Models;
using AutoMapper;

namespace my_clinic_api.Profiles
{
    public class DoctorTestProfile : Profile
    {
        public DoctorTestProfile()
        {
            CreateMap<Specialist, Dto.SpecialistsDto>();

        }
    }
}
