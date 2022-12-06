using my_clinic_api.Dto.AuthDtos;
using my_clinic_api.Models;

namespace my_clinic_api.Interfaces
{
    public interface IAuthService
    {
        Task<ApplicationUser> UserRegisterAsync(UserRegisterDto userDto);
        Task<Doctor> DoctorRegisterAsync(DoctorRegisterDto doctorDto);


    }
}
