using my_clinic_api.Interfaces;
using my_clinic_api.Models;
using System.Linq.Expressions;

namespace my_clinic_api.Services
{
    public class SpecialistService : BaseRepository<Specialist>, ISpecialistService
    {
        private readonly IDoctorService _doctorService;
        public SpecialistService(ApplicationDbContext Context, IDoctorService doctorService) : base(Context)
        {
            _doctorService = doctorService;
        }

        public async Task<bool> AddSpecialistToDoctor(string doctorId, int specialistId)
        {
            var specialist = await FindByIdAsync(specialistId);

            var doctor = await _doctorService.FindDoctorByIdWithDataAsync(doctorId);
            if (doctor == null)
                return false;
            doctor.Specialists.Add(new Specialist { Id = specialistId , SpecialistName = specialist.SpecialistName});
            CommitChanges();
            return true;
        }
        
    }
}