using Microsoft.EntityFrameworkCore;
using my_clinic_api.Interfaces;
using my_clinic_api.Models;
using System.Linq.Expressions;

namespace my_clinic_api.Services
{
    public class DepartmentService : BaseRepository<Department>, IDepartmentService
    {
        public DepartmentService(ApplicationDbContext Context) : base(Context)
        {
        }


        public async Task<IEnumerable<Department>> DepartmentNameIsExist(string departmentName)
        {
            Expression<Func<Department, bool>> predicate = h => h.Name.Equals(departmentName);
            var department = await FindAllAsync(predicate);
            if (department.Any()) return department;
            return Enumerable.Empty<Department>();
        }
    }
}
