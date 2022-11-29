using my_clinic_api.Interfaces;
using my_clinic_api.Models;

namespace my_clinic_api.Services
{
    public class HospitalService : BaseRepository<Hospital>, IHospitalService
    {
       // private readonly ApplicationDbContext _Context;
        public HospitalService(ApplicationDbContext Context) : base(Context)
        {
        }

        public IEnumerable<Hospital> LargeHospitals()
        {
            throw new NotImplementedException();
        }
    }
}
