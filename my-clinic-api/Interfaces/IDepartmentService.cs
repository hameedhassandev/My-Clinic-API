using my_clinic_api.Models;

namespace my_clinic_api.Interfaces
{
    public interface IDepartmentService : IBaseRepository<Department>

    {
        Task<Department> FindDepartmentByIdWithData(int departmentId);
        Task<IEnumerable<Department>> DepartmentNameIsExist(string departmentName);
        Task<bool> IsSpecialistInDepartment(int deptmentId , List<int> specialistsIds);
        Task<bool> DepartmentIsExists(int departmentId);
    }
}
