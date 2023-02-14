using Microsoft.AspNetCore.Identity;
using my_clinic_api.Dto.AuthDtos;
using my_clinic_api.DTOS;
using my_clinic_api.DTOS.AuthDtos;
using my_clinic_api.Models;

namespace my_clinic_api.Interfaces
{
    public interface IAuthService
    {
        Task<AuthModelDto> UserRegisterAsync(UserRegisterDto userDto);
        Task<AuthModelDto> DoctorRegisterAsync(DoctorRegisterDto doctorDto,bool isConfirmedFromAdmin);
        Task<AuthModelDto> GetTokenAsync(TokenRequestModelDto modelDto);
        Task<AuthModelDto> ConfirmDoctor(string doctorId);
        Task<AuthModelDto> testRegisteration(DoctorRegisterDto doctorDto);
        Task<AuthModelDto> updateProfilePic(updateImageDto dto);

        Task<AuthModelDto> updatDoctor(updateDoctorDto dto);
        Task<bool> ConfirmUserEmail(confirmMailDto dto);
        Task<List<IdentityRole>> GetRoles();
        Task<bool> AddRole(string roleName);

        Task<bool> confirmationMailDoctor(string doctorId);


    }
}
