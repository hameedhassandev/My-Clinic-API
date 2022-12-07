using my_clinic_api.Dto.AuthDtos;
using my_clinic_api.Models;

namespace my_clinic_api.Interfaces
{
    public interface IAuthService
    {
        Task<AuthModelDto> UserRegisterAsync(UserRegisterDto userDto);
        Task<Doctor> DoctorRegisterAsync(DoctorRegisterDto doctorDto);


    }
}
