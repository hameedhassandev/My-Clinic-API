using AutoMapper;
using my_clinic_api.Models;

namespace my_clinic_api.Profiles
{
    public class RateAndReviewProfile : Profile
    {
        public RateAndReviewProfile()
        {
            CreateMap<RateAndReview , DTOS.RateAndReviewDto>();
        }
    }
}
