using AutoMapper;
using my_clinic_api.Models;

namespace my_clinic_api.Profiles
{
    public class InsuranceProfile : Profile
    {
        public InsuranceProfile()
        {
            CreateMap<Insurance, DTOS.InsuranceDto>();
        }
    }
}
