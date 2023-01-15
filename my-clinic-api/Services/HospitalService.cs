using Microsoft.EntityFrameworkCore;
using my_clinic_api.Interfaces;
using my_clinic_api.Models;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace my_clinic_api.Services
{
    public class HospitalService : BaseRepository<Hospital>, IHospitalService
    {
        private readonly IComparer2Lists _comparer2Lists;
        private readonly IDoctorService _doctorService;
        private readonly ApplicationDbContext _context;
        public HospitalService(ApplicationDbContext Context, IComparer2Lists comparer2Lists, IDoctorService doctorService, ApplicationDbContext context) : base(Context)
        {
            _comparer2Lists = comparer2Lists;
            _doctorService = doctorService;
            _context = context;
        }

        public async Task<Hospital> FindHospitalByIdWithData(int hospitalId)
        {
            var data = GetCollections(typeof(Hospital));
            Expression<Func<Hospital, bool>> criteria = d => d.Id == hospitalId;
            var hospital = await FindWithData(criteria);
            return hospital;
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

        public async Task<bool> IsHospitalIdsIsExist(List<int> doctorHospitalIds)
        {
            var allHospital = await GetAllAsync();
            var allHospitalIds = allHospital.Select(s => s.Id).ToList();
            var compare = _comparer2Lists.Comparer2IntLists(allHospitalIds, doctorHospitalIds);
            return compare;
        }

        public async Task<Doctor> AddHospitalToDoctor(List<int> HospitalsIds, Doctor doctor)
        {
            doctor.Hospitals ??= new Collection<Hospital>();
            foreach (int hospitalId in HospitalsIds)
            {
                var Hospital = await FindByIdAsync(hospitalId);
                doctor.Hospitals.Add(Hospital);
            }
            return doctor;
        }



    }
}
