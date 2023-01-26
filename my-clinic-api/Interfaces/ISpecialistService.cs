using my_clinic_api.Models;

namespace my_clinic_api.Interfaces
{
    public interface ISpecialistService : IBaseRepository<Specialist>
    {
        Task<Specialist> FindSpecialistByIdWithData(int specialistId);
        Task<IEnumerable<Specialist>> FindSpecialistByDepartmentId(int departmentId);

        Task<IEnumerable<Specialist>> GetAllSpecialistWithDepartment();
        Task<Doctor> AddSpecialistToDoctor(List<int> SpecialistsIds, Doctor doctor );
    }
}