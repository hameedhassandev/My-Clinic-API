using my_clinic_api.DTOS;
using my_clinic_api.Models;

namespace my_clinic_api.Interfaces
{
    public interface IReportService : IBaseRepository<Report>
    {
        Task<Report> FindByIdWithData(int reportId);
        Task<Report> AddReport(ReportDto reportDto);
    }
}
