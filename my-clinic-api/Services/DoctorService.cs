using Microsoft.EntityFrameworkCore;
using my_clinic_api.Dto;
using my_clinic_api.DTOS;
using my_clinic_api.Interfaces;
using my_clinic_api.Models;
using System.Linq.Expressions;

namespace my_clinic_api.Services
{
    public class DoctorService : BaseRepository<Doctor>, IDoctorService
    {
        private readonly ApplicationDbContext _context;

        public DoctorService(ApplicationDbContext Context) : base(Context)
        {
            _context = Context;

        }

        public async Task<Doctor> FindDoctorByIdAsync(string doctorId)
        {

            var doctor = await _context.doctors.Include(d => d.Department)
                .Include(s => s.Specialists)
                .Include(i => i.Insurances)
                .Include(h => h.Hospitals)
                .Include(a => a.Area)
                .Include(r => r.RatesAndReviews).ThenInclude(u=> u.Patient )
                .FirstOrDefaultAsync(d => d.Id == doctorId);
                

            return doctor;
        }

      public async Task<IEnumerable<Doctor>> GetAllDoctorAsync()
        {
            var allDoctors = await _context.doctors.Include(d => d.Department)
               .Include(s => s.Specialists)
               .Include(i => i.Insurances)
               .Include(h => h.Hospitals)
               .Include(a => a.Area)
               .Include(r => r.RatesAndReviews).ThenInclude(u => u.Patient)
               .ToListAsync();


            return allDoctors;
        }

    }
}
