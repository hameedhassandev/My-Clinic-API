using my_clinic_api.Models;

namespace my_clinic_api.Interfaces
{
    public interface IAuthService
    {
        Task<AuthModel> RegisterAsync(RegisterModel model);
            

    }
}
