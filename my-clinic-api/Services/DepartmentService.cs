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

        public async Task<bool> DepartmentIsExists(int departmentId)
        {
            var deparment = await GetByIdAsync(departmentId);
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
            var specialist = await GetByIdWithIncludeAsync(deptmentId , "specialists" , "Collection");
            var SpeList = specialist.specialists.Select(s=>s.Id).ToList();
            var compare = Comparer2Lists(SpeList, specialistsIds);
            return compare;
        }

        private bool Comparer2Lists (List<int> OrgList , List<int> DocList)
        {
            int counter = 0;
            foreach(var item in DocList)
            {
                foreach(var item2 in OrgList)
                {
                    if (item == item2)
                    {
                        counter++;
                    }
                }
            }
            if (counter != DocList.Count())
                return false;
            return true;

        }
    }
}
