using AutoMapper;
using my_clinic_api.DTOS;
using my_clinic_api.Models;

namespace my_clinic_api.Profiles
{
    public class ReportProfile : Profile
    {
        public ReportProfile()
        {
            CreateMap<Report, ReportDto>();
        }
    }
}
