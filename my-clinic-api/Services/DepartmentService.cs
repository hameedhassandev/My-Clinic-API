using Microsoft.EntityFrameworkCore;
using my_clinic_api.Interfaces;
using my_clinic_api.Models;
using System.Linq.Expressions;

namespace my_clinic_api.Services
{
    public class DepartmentService : BaseRepository<Department>, IDepartmentService
    {
        private readonly IComparer2Lists _comparer2Lists;
        public DepartmentService(ApplicationDbContext Context, IComparer2Lists comparer2Lists) : base(Context)
        {
            _comparer2Lists = comparer2Lists;
        }

        public async Task<bool> DepartmentIsExists(int departmentId)
        {
            var deparment = await FindByIdAsync(departmentId);
            if (deparment is not null)
                return true;
            return false;
        }

        public async Task<IEnumerable<Department>> DepartmentNameIsExist(string departmentName)
        {
            Expression<Func<Department, bool>> predicate = h => h.Name.Equals(departmentName);
            var department = await FindAllAsync(predicate);
            if (department.Any()) return department;
            return Enumerable.Empty<Department>();
        }

        public async Task<bool> IsSpecialistInDepartment(int deptmentId , List<int> specialistsIds)
        {
            var isExists = await DepartmentIsExists(deptmentId);
            if (!isExists)
                return false ;
            var specialist = await FindByIdWithIncludeAsync(deptmentId , "specialists" , "Collection");
            var SpeList = specialist.specialists.Select(s=>s.Id).ToList();
            var compare = _comparer2Lists.Comparer2IntLists(SpeList, specialistsIds);
            return compare;
        }

     
    }
}
