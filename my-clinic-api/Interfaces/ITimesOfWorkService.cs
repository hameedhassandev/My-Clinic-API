using my_clinic_api.Models;

namespace my_clinic_api.Interfaces
{
    public interface ITimesOfWorkService : IBaseRepository<TimesOfWork> 
    {
        Task<IEnumerable<TimesOfWork>> GetTimesOfDoctor(string doctorId);
    }
}
