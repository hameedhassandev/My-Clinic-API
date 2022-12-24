using my_clinic_api.Models;

namespace my_clinic_api.Interfaces
{
    public interface ISpecialistService : IBaseRepository<Specialist>
    {
        Task<Specialist> FindSpecialistByIdWithData(int specialistId);
        Task<bool> AddSpecialistToDoctor(string doctorId , int specialistId);
    }
}