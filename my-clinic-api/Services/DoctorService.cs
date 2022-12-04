using Microsoft.EntityFrameworkCore;
using my_clinic_api.Interfaces;
using my_clinic_api.Models;
using System.Linq.Expressions;

namespace my_clinic_api.Services
{
    public class DoctorService : BaseRepository<Doctor>, IDoctorService
    {
        public DoctorService(ApplicationDbContext Context) : base(Context)
        {
        }

        public async Task<Doctor> FindDoctorByIdSync(string userId)
        {
            Expression<Func<Doctor, bool>> criteria = h => h.Id == userId;
            return await FindAsync(criteria);
            
        }
    }
}
