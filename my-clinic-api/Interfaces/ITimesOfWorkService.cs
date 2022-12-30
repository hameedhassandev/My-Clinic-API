using my_clinic_api.DTOS;
using my_clinic_api.DTOS.CreateDto;
using my_clinic_api.Models;

namespace my_clinic_api.Interfaces
{
    public interface ITimesOfWorkService : IBaseRepository<TimesOfWork> 
    {
        Task<TimesOfWork> FindTimeByIdWithData(int timeId);
        Task<IEnumerable<TimesOfWork>> GetTimesOfDoctor(string doctorId);
        Task<bool> TimeIsAvailable(CreateBookDto bookDto);
        IList<TimeSpan> GetDoctorAllTimesSpans(TimeSpan start, TimeSpan end, int waitingTime);
        public string GetNextDaysFromNow(int TimeToAdd);
        Task<string> AddTimetoDoctor(CreateTimesOfWorkDto dto);
    }
}
