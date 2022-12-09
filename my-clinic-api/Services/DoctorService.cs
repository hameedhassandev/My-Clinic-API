using Microsoft.EntityFrameworkCore;
using my_clinic_api.Dto;
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

        public async Task<Doctor> FindDoctorByIdAsync(string userId)
        {
         
            var doctor = await _context.doctors.Include(d => d.Department)
                .Include(s => s.Specialists)
                .Include(i => i.Insurances)
                .Include(h => h.Hospitals)
                .Include(a => a.Area)
                .Include(r => r.RatesAndReviews)
                .FirstOrDefaultAsync(d => d.Id == userId);

            return doctor;
        }
    }
}
