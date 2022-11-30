using my_clinic_api.Interfaces;
using my_clinic_api.Models;
using System.Linq.Expressions;

namespace my_clinic_api.Services
{
    public class HospitalService : BaseRepository<Hospital>, IHospitalService
    {
        public HospitalService(ApplicationDbContext Context) : base(Context)
        {
          
        }

        public async Task<bool> HospitalIsExist(string hospitalName)
        {
            Expression<Func<Hospital, bool>> predicate = h => h.Name.Equals(hospitalName);

            var allHospital = await FindAllAsync(predicate);

              if (allHospital.Any()) return true;
            return false;
        }
    }
}
