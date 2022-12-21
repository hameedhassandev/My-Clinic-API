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
        public async Task<IEnumerable<Patient>> GetAllPatientsWithData()
        {
            var patients = await GetAllWithData();
            if (patients == null) return Enumerable.Empty<Patient>();
            return patients;
        }

        public async Task<Patient> FindPatientByIdAsync(string patientId)
        {
            Expression<Func<Patient , bool>> predicate = h => h.Id == patientId;
            var Patient = await FindAsync(predicate);
            return Patient;
        }
        public async Task<Patient> FindPatientByIdWithData(string patientId)
        {
            Expression<Func<Patient , bool>> predicate = h => h.Id == patientId;
            var data = GetCollections(typeof(Patient));
            var Patient = await FindWithIncludesAsync(predicate, data );
            return Patient;
        }
    }
}
