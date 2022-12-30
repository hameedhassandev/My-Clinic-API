using Microsoft.AspNetCore.Mvc;
using my_clinic_api.DTOS;
using my_clinic_api.DTOS.CreateDto;
using my_clinic_api.Interfaces;
using my_clinic_api.Models;
using System.Linq.Expressions;

namespace my_clinic_api.Services
{
    public class ReportService : BaseRepository<Report> , IReportService 
    {
        public ReportService(ApplicationDbContext Context) : base(Context)
        {
        }

        public async Task<Report> FindByIdWithData(int reportId)
        {
            Expression<Func<Report, bool>> criteria = d => d.Id == reportId;
            var report = await FindWithData(criteria);
            return report;
        }

        public async Task<Report> AddReport(CreateReportDto  reportDto)
        {
            var report = new Report
            {
                Description = reportDto.Description,
                ReasonId = reportDto.ReasonId,
                DoctorId = reportDto.DoctorId,
                PatientId = reportDto.PatientId
            };

            var result = await AddAsync(report);
            CommitChanges();
            return result;

        }
    }
}
