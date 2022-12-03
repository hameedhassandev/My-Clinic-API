using Microsoft.EntityFrameworkCore;
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

        public async Task<Doctor> FindDoctorByIdSync(string userId)
        {
            var doctor = await _context.doctors.FirstOrDefaultAsync(d => d.Id == userId);
            return doctor;
        }
    }
}
