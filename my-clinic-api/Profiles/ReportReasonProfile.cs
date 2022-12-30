using AutoMapper;
using my_clinic_api.Models;

namespace my_clinic_api.Profiles
{
    public class ReportReasonProfile : Profile
    {
        public ReportReasonProfile()
        {
            CreateMap<ReportReasons, DTOS.ReportReasonsDto>();
        }
    }
}
