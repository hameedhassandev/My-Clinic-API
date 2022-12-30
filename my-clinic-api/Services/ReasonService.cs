using my_clinic_api.Interfaces;
using my_clinic_api.Models;
using System.Linq.Expressions;

namespace my_clinic_api.Services
{
    public class ReasonService : BaseRepository<ReportReasons>, IReasonService
    {
        public ReasonService(ApplicationDbContext Context) : base(Context)
        {
        }
        public async Task<ReportReasons> FindByIdWithData(int reasonId)
        {
            Expression<Func<ReportReasons, bool>> criteria = d => d.Id == reasonId;
            var reason = await FindWithData(criteria);
            return reason;
        }
    }
}
