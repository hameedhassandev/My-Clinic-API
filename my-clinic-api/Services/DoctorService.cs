using Microsoft.EntityFrameworkCore;
using my_clinic_api.Dto;
using my_clinic_api.DTOS;
using my_clinic_api.Interfaces;
using my_clinic_api.Models;
using System.Linq.Expressions;

namespace my_clinic_api.Services
{
    public class DoctorService : BaseRepository<Doctor>, IDoctorService
    {
        private readonly ApplicationDbContext _context;

        public DoctorService(ApplicationDbContext Context) : base(Context)
        {
            _context = Context;

        }
        public async Task<IEnumerable<Doctor>> GetAllDoctorWithDataAsync()
        {
            var data = GetCollections(typeof(Doctor));

            var doctors = await GetAllWithIncludeAsync(data);

            if (doctors == null) return Enumerable.Empty<Doctor>();
            return doctors;
        }

        public async Task<Doctor> FindDoctorByIdAsync(string doctorId)
        {
            Expression<Func<Doctor, bool>> criteria = d=>d.Id == doctorId;
            var doctor = await FindAsync(criteria);
            return doctor;
        }

        public async Task<Doctor> FindDoctorByIdWithDataAsync(string doctorId)
        {
            var data = GetCollections(typeof(Doctor));
            Expression<Func<Doctor, bool>> criteria = d => d.Id == doctorId;
            var doctor = await FindWithIncludesAsync(criteria,data);
            return doctor;
        }
        

        public async Task<int> GetWaitingTimeOfDoctor(string Id)
        {
            Expression<Func<Doctor, bool>> expression = h => h.Id == Id;
            var doctor = await FindAsync(expression);
            return doctor.WaitingTime;
        }

    }
}
