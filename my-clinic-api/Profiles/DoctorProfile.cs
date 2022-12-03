using AutoMapper;

namespace my_clinic_api.Profiles
{
    public class DoctorProfile : Profile
    {
        public DoctorProfile()
        {
            CreateMap<Models.Doctor, Dto.DoctorDto>();
        }
    }

}
