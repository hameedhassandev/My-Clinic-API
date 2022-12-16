using my_clinic_api.Interfaces;
using my_clinic_api.Models;
using System.Linq.Expressions;

namespace my_clinic_api.Services
{
    public class TimesOfWorkService : BaseRepository<TimesOfWork>, ITimesOfWorkService
    {
        public TimesOfWorkService(ApplicationDbContext Context) : base(Context)
        {
        }

        public async Task<IEnumerable<TimesOfWork>> GetTimesOfDoctor(string doctorId)
        {
            Expression<Func<TimesOfWork, bool>> predicate = h => h.doctorId == doctorId;
            var times = await FindAllAsync(predicate);
            if (times.Any()) return times;
            return Enumerable.Empty<TimesOfWork>();
        }
    }
}
