using Microsoft.EntityFrameworkCore;
using my_clinic_api.Interfaces;
using my_clinic_api.Models;
using System.Linq.Expressions;

namespace my_clinic_api.Services
{
    public class PatientService : BaseRepository<Patient>, IPatientService
    {
        public PatientService(ApplicationDbContext Context) : base(Context)
        {
        }

        public async Task<Patient> FindPatientByIdAsync(string patientId)
        {
            Expression<Func<Patient , bool>> predicate = h => h.Id == patientId;
            var Patient = await FindAsync(predicate);
            return Patient;
        }
        public async Task<Patient> FindPatientByIdWithIncludeAsync(string patientId)
        {
            Expression<Func<Patient , bool>> predicate = h => h.Id == patientId;
            var Patient = await FindWithIncludesAsync(predicate, new List<string> { "RateAndReviews", "Bookings" });
            return Patient;
        }
    }
}
