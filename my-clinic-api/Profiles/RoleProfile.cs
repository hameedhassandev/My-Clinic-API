using AutoMapper;
using Microsoft.AspNetCore.Identity;
using my_clinic_api.Models;

namespace my_clinic_api.Profiles
{
    public class RoleProfile : Profile
    {
            public RoleProfile()
            {
                CreateMap<IdentityRole, DTOS.AuthDtos.RolesDto>();
            }
    }
}
