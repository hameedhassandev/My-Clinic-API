using my_clinic_api.Interfaces;
using my_clinic_api.Models;

namespace my_clinic_api.Services
{
    public class SpecialistService : BaseRepository<Specialist>, ISpecialistService
    {
        public SpecialistService(ApplicationDbContext Context) : base(Context)
        {
        }
    }
}
