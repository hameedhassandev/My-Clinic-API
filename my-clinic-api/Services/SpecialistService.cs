using Microsoft.EntityFrameworkCore;
using my_clinic_api.Interfaces;
using my_clinic_api.Models;
using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace my_clinic_api.Services
{
    public class SpecialistService : BaseRepository<Specialist>, ISpecialistService
    {
        private readonly IDoctorService _doctorService;
        private readonly ApplicationDbContext _context;
        public SpecialistService(ApplicationDbContext Context, IDoctorService doctorService) : base(Context)
        {
            _doctorService = doctorService;
            _context = Context; 
        }

        public async Task<Specialist> FindSpecialistByIdWithData(int specialistId)
        {
            var data = GetCollections(typeof(Specialist));
            Expression<Func<Specialist, bool>> criteria = d => d.Id == specialistId;
            var specialist = await FindWithData(criteria);
            return specialist;
        }

        public async Task<IEnumerable<Specialist>> FindSpecialistByDepartmentId(int departmentId)
        {
            Expression<Func<Specialist, bool>> criteria = d => d.departmentId == departmentId;
            var specialistsByDepId = await FindAllAsync(criteria);
            return specialistsByDepId;
        }

        public async Task<IEnumerable<Specialist>> GetAllSpecialistWithDepartment()
        {
            var allSpecialistsWithDep = await _context.Specialists.Include(d => d.department).ToListAsync();

            return allSpecialistsWithDep;   
        }

        public async Task<Doctor> AddSpecialistToDoctor(List<int> SpecialistsIds, Doctor doctor)
        {
            doctor.Specialists ??= new Collection<Specialist>();
            foreach (int specialistId in SpecialistsIds)
            {
                var specialist = await FindByIdAsync(specialistId);
                doctor.Specialists.Add(specialist);
            }
            return doctor;
        }

   


    }
}