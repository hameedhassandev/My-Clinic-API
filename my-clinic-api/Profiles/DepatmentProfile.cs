using AutoMapper;
using my_clinic_api.Models;

namespace my_clinic_api.Profiles
{
    public class DepatmentProfile : Profile
    {
        public DepatmentProfile()
        {
            CreateMap<Department, DTOS.DepartmentDto>();        
        }
    }
}
