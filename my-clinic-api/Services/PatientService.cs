using my_clinic_api.Interfaces;
using my_clinic_api.Models;

namespace my_clinic_api.Services
{
    public class PatientService : BaseRepository<Patient>, IPatientService
    {
        public PatientService(ApplicationDbContext Context) : base(Context)
        {
        }
    }
}
