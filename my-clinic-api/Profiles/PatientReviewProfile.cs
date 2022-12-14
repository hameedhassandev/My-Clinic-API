using AutoMapper;
using my_clinic_api.Models;

namespace my_clinic_api.Profiles
{
    public class PatientReviewProfile : Profile
    {
        public PatientReviewProfile()
        {
            CreateMap<Patient, DTOS.PatientReviewDto>();
        }
    }
}
