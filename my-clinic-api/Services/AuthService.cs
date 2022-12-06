using my_clinic_api.Dto.AuthDtos;
using my_clinic_api.Interfaces;
using my_clinic_api.Models;

namespace my_clinic_api.Services
{
    public class AuthService : IAuthService
    {
        public Task<Doctor> DoctorRegisterAsync(DoctorRegisterDto doctorDto)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> UserRegisterAsync(UserRegisterDto userDto)
        {
            throw new NotImplementedException();
        }
    }
}
