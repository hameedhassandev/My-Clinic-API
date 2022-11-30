using my_clinic_api.Interfaces;
using my_clinic_api.Models;
using System.Collections;
using System.Linq.Expressions;

namespace my_clinic_api.Services
{
    public class HospitalService : BaseRepository<Hospital>, IHospitalService
    {
        public HospitalService(ApplicationDbContext Context) : base(Context)
        {
          
        }

        public async Task<IEnumerable<Hospital>> HospitalNameIsExist(string hospitalName)
        {
            Expression<Func<Hospital, bool>> predicate = h => h.Name.Equals(hospitalName);
            var hospital = await FindAllAsync(predicate);
              if (hospital.Any()) return hospital;
            return Enumerable.Empty<Hospital>();
        }
        public async Task<IEnumerable<Hospital>> HospitalAddressIsExist(string hospitalAddress)
        {
            Expression<Func<Hospital, bool>> predicate = h => h.Address.Equals(hospitalAddress);
            var hospital = await FindAllAsync(predicate);
              if (hospital.Any()) return hospital.ToList();
            return Enumerable.Empty<Hospital>();
        }

    }
}
