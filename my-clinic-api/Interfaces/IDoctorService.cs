using my_clinic_api.Models;

namespace my_clinic_api.Interfaces
{
    public interface IDoctorService : IBaseRepository<Doctor>
    {
        Task<Doctor> FindDoctorByIdAsync(string doctorId);
        Task<IEnumerable<Doctor>> GetAllDoctorAsync();
        Task<int> GetWaitingTimeOfDoctor(string Id);

    }

}
