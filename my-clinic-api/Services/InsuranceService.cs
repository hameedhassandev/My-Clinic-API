using Microsoft.EntityFrameworkCore;
using my_clinic_api.Interfaces;
using my_clinic_api.Models;
using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace my_clinic_api.Services
{
    public class InsuranceService : BaseRepository<Insurance>, IInsuranceService
    {
        private readonly IComparer2Lists _comparer2Lists;
        private readonly IDoctorService _doctorService;
        private readonly ApplicationDbContext _context;

        public InsuranceService(ApplicationDbContext Context, IComparer2Lists comparer2Lists, IDoctorService doctorService, ApplicationDbContext context) : base(Context)
        {
            _comparer2Lists = comparer2Lists;
            _doctorService = doctorService;
            _context = context;
        }

        public async Task<Insurance> FindInsuranceByIdWithData(int insuranceId)
        {
            var data = GetCollections(typeof(Insurance));
            Expression<Func<Insurance, bool>> criteria = d => d.Id == insuranceId;
            var insurance = await FindWithData(criteria);
            return insurance;
        }
        public async Task<Doctor> AddInsuranceToDoctor(List<int> InsuranceIds, Doctor doctor)
        {
            doctor.Insurances ??= new Collection<Insurance>();
            foreach (int insuranceId in InsuranceIds)
            {
                var Insurance = await FindByIdAsync(insuranceId);
                doctor.Insurances.Add(Insurance);
            }
            return doctor;
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
