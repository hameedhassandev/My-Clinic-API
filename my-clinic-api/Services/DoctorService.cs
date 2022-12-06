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
            Expression<Func<Doctor, bool>> predicate = h => h.Id == userId;

            var doctor = await FindAsync(predicate);
            if (doctor == null)
                return null;
            return doctor;
        }


        
    }
}
