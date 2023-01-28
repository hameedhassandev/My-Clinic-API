using my_clinic_api.DTOS;
using my_clinic_api.DTOS.CreateDto;
using my_clinic_api.Interfaces;
using my_clinic_api.Models;
using System.Linq.Expressions;

namespace my_clinic_api.Services
{
    public class TimesOfWorkService : BaseRepository<TimesOfWork>, ITimesOfWorkService
    {
        private readonly IDoctorService _doctorService;
        public TimesOfWorkService(ApplicationDbContext Context, IDoctorService doctorService) : base(Context)
        {
            _doctorService = doctorService;
        }


        public async Task<TimesOfWork> FindTimeByIdWithData(int timeId)
        {
            var data = GetCollections(typeof(TimesOfWork));
            Expression<Func<TimesOfWork, bool>> criteria = d => d.Id == timeId;
            var time = await FindWithData(criteria);
            return time;
        }

        public async Task<IEnumerable<TimesOfWork>> GetTimesOfDoctor(string doctorId)
        {
            Expression<Func<TimesOfWork, bool>> predicate = h => h.doctorId == doctorId;
            var times = await FindAllAsync(predicate);
            if (times.Any()) return times;
            return Enumerable.Empty<TimesOfWork>();
        }

        public async Task<bool> TimeIsAvailable(CreateBookDto bookDto)
        {
            var day = bookDto.Time.DayOfWeek;
            var times = await GetTimesOfDoctor(bookDto.DoctorId);
            if (!times.Any()) return false;
            var check = times.SingleOrDefault(d=>d.day.ToString()==day.ToString());
            if (check is null) return false;
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

        public IList<TimeSpan> GetDoctorAllTimesSpans(TimeSpan start , TimeSpan end , int waitingTime )
        {
            IList<TimeSpan> Times = new List<TimeSpan>();
            for(TimeSpan i = start; i < end ; i= i.Add(TimeSpan.FromMinutes(waitingTime)))
            {
                Times.Add(i);
            }
            return Times;
        }

        public async Task<string> AddTimetoDoctor(CreateTimesOfWorkDto dto)
        {
            var doc = await _doctorService.FindDoctorByIdAsync(dto.doctorId);
            if (doc is null) return "No doctor found with this Id";
            Expression<Func<TimesOfWork, bool>> criteria = t => t.doctorId == dto.doctorId && t.day == dto.day;
            var DayIsExist = await FindAsync(criteria);
            if (DayIsExist is not null) return "This day is already exists";
            var time = new TimesOfWork
            {
                day = dto.day,
                StartWork = dto.StartWork,
                EndWork = dto.EndWork,
                doctorId = dto.doctorId
            };
            var result = await AddAsync(time);
            CommitChanges();
            return ("Success");
        }
    }
}
