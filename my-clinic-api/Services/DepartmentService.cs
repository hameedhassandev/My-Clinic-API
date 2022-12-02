using Microsoft.EntityFrameworkCore;
using my_clinic_api.Interfaces;
using my_clinic_api.Models;
using System.Linq.Expressions;

namespace my_clinic_api.Services
{
    public class DepartmentService : BaseRepository<Department>, IDepartmentService
    {
        private readonly ApplicationDbContext _context;
        public DepartmentService(ApplicationDbContext Context) : base(Context)
        {
            _context = Context;
        }


        public async Task<IEnumerable<Department>> GetAllWithSpecialistAsync()
        {
            var DepartmentWithSpecialist = await _context.Departments.Include(d => d.specialists).ToListAsync();
            return DepartmentWithSpecialist;    
        }
    }
}
