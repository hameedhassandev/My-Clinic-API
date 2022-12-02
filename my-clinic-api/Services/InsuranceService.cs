using my_clinic_api.Interfaces;
using my_clinic_api.Models;
using System.Linq.Expressions;

namespace my_clinic_api.Services
{
    public class InsuranceService : BaseRepository<Insurance>, IInsuranceService
    {
        public InsuranceService(ApplicationDbContext Context) : base(Context)
        {
        }

        public async Task<IEnumerable<Insurance>> InsuranceNameIsExist(string insuranceName)
        {
            Expression<Func<Insurance, bool>> predicate = h => h.CompanyName.Equals(insuranceName);
            var insurance = await FindAllAsync(predicate);
            if (insurance.Any()) return insurance;
            return Enumerable.Empty<Insurance>();
        }
    }
}
