using AutoMapper;
using my_clinic_api.Models;

namespace my_clinic_api.Profiles
{
    public class UserReviewProfile : Profile
    {
        public UserReviewProfile()
        {
            CreateMap<ApplicationUser, DTOS.UserReviewDto>();
        }
    }
}
