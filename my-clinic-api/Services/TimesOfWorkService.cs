using my_clinic_api.DTOS;
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

        public async Task<TimesOfWork> FindTimeByIdWithData(int timeId)
        {
            var data = GetCollections(typeof(TimesOfWork));
            Expression<Func<TimesOfWork, bool>> criteria = d => d.Id == timeId;
            var time = await FindWithIncludesAsync(criteria, data);
            return time;
        }

        public async Task<IEnumerable<TimesOfWork>> GetTimesOfDoctor(string doctorId)
        {
            Expression<Func<TimesOfWork, bool>> predicate = h => h.doctorId == doctorId;
            var times = await FindAllAsync(predicate);
            if (times.Any()) return times;
            return Enumerable.Empty<TimesOfWork>();
        }

        public async Task<bool> TimeIsAvailable(BookDto bookDto)
        {
            var day = bookDto.Time.DayOfWeek;
            var times = await GetTimesOfDoctor(bookDto.DoctorId);
            var check = times.SingleOrDefault(d=>d.day.ToString()==day.ToString());
            var start = TimeSpan.Compare(check.StartWork.TimeOfDay , bookDto.Time.TimeOfDay);
            var end = TimeSpan.Compare(check.EndWork.TimeOfDay , bookDto.Time.TimeOfDay);
            return ((start == -1 || start == 0) && (end == 1));
        }

        public string GetNextDaysFromNow(int TimeToAdd)
        {
            var EnumValueToAdd = (((int)DateTime.Now.DayOfWeek) + TimeToAdd) % 7;
            var check = Enum.GetName(typeof(DayOfWeek), EnumValueToAdd);
            return check;
        }
    }
}
