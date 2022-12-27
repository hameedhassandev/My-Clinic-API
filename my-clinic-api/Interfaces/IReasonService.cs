using my_clinic_api.Models;

namespace my_clinic_api.Interfaces
{
    public interface IReasonService : IBaseRepository<ReportReasons>
    {
        Task<ReportReasons> FindByIdWithData(int reasonId);
    }
}
