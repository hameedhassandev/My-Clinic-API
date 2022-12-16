using AutoMapper;
using my_clinic_api.Models;

namespace my_clinic_api.Profiles
{
    public class TimesOfWorkProfile : Profile
    {
        public TimesOfWorkProfile()
        {
            CreateMap<TimesOfWork, DTOS.TimesOfWorkDto>();
        }
    }
}
