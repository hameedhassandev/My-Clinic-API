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
        public DoctorService(ApplicationDbContext Context) : base(Context)
        {
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
            var doctor = await FindWithData(criteria);
            return doctor;
        }
        

        public async Task<int> GetWaitingTimeOfDoctor(string Id)
        {
            Expression<Func<Doctor, bool>> expression = h => h.Id == Id;
            var doctor = await FindAsync(expression);
            return doctor.WaitingTime;
        }

        public async Task<IEnumerable<Doctor>> GetAllConfirmedDoctors()
        {
            var allDoctors = await FindAllWithData(d => d.EmailConfirmed);
            if (allDoctors == null)
                return Enumerable.Empty<Doctor>();
            return allDoctors;
        }
        public async Task<IEnumerable<Doctor>> GetAllNotConfirmedDoctors()
        {
            var allDoctors = await FindAllWithData(d => !d.EmailConfirmed);
            if (allDoctors == null)
                return Enumerable.Empty<Doctor>();
            return allDoctors;
        }

        public async Task<IEnumerable<Doctor>> GetAllDoctorsWithFiltercriteria(FilterDto dto)
        {

            Expression<Func<Doctor, bool>> expression;
            if (dto.city == null)
            {

                expression = h => h.DepartmentId == dto.department;
            }
            else if (dto.department == null)
            {
                expression = h => (int)h.Cities == dto.city;
            }
            else
            {
                expression = h => h.DepartmentId == dto.department && (int)h.Cities == dto.city;

            }
            expression = c => c.EmailConfirmed;
            var doctors = await GetAllPaginationAsyncWithData(expression);
            return doctors;
        }
    }
}
