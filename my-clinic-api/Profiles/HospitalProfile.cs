using AutoMapper;
using my_clinic_api.Models;

namespace my_clinic_api.Profiles
{
    public class HospitalProfile : Profile
    {
        public HospitalProfile()
        {
            CreateMap<Hospital, DTOS.HospitalDto>();
        }
    }
}
