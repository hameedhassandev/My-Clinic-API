using my_clinic_api.Models;

namespace my_clinic_api.Interfaces
{
    public interface IDoctorService : IBaseRepository<Doctor>
    {
         Task<Doctor> FindDoctorByIdSync(string userId);
    }
}
