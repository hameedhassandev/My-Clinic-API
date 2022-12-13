using my_clinic_api.Interfaces;
using my_clinic_api.Models;
using System.Linq.Expressions;

namespace my_clinic_api.Services
{
    public class InsuranceService : BaseRepository<Insurance>, IInsuranceService
    {
        private readonly IComparer2Lists _comparer2Lists;
        private readonly IDoctorService _doctorService;

        public InsuranceService(ApplicationDbContext Context, IComparer2Lists comparer2Lists, IDoctorService doctorService) : base(Context)
        {
            _comparer2Lists = comparer2Lists;   
            _doctorService = doctorService; 
        }

        public async Task<bool> AddInsuranceToDoctor(string doctorId, int InsurancelId)
        {

            var Insurance = await GetByIdAsync(InsurancelId);

            var doctor = await _doctorService.FindDoctorByIdAsync(doctorId);
            if (doctor == null)
                return false;

            doctor.Insurances.Add(new Insurance { Id = InsurancelId,CompanyName = Insurance.CompanyName,Discount = Insurance.Discount });

            CommitChanges();
            return true;
        }

        public async Task<IEnumerable<Insurance>> InsuranceNameIsExist(string insuranceName)
        {
            Expression<Func<Insurance, bool>> predicate = h => h.CompanyName.Equals(insuranceName);
            var insurance = await FindAllAsync(predicate);
            if (insurance.Any()) return insurance;
            return Enumerable.Empty<Insurance>();
        }

        public async Task<bool> IsInsuranceIdsIsExist(List<int> doctorInsuranceIds)
        {
            var allInsurance = await GetAllAsync();
            var allInsuranceIds = allInsurance.Select(s => s.Id).ToList();
            var compare = _comparer2Lists.Comparer2IntLists(allInsuranceIds, doctorInsuranceIds);
            return compare;
        }
    }
}
