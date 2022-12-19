using my_clinic_api.DTOS;
using my_clinic_api.Models;

namespace my_clinic_api.Interfaces
{
    public interface ITimesOfWorkService : IBaseRepository<TimesOfWork> 
    {
        Task<IEnumerable<TimesOfWork>> GetTimesOfDoctor(string doctorId);
        Task<bool> TimeIsAvailable(BookDto bookDto);
        public string GetNextDaysFromNow(int TimeToAdd);
    }
}
