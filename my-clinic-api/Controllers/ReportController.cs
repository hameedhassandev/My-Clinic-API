using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_clinic_api.Classes;
using my_clinic_api.DTOS;
using my_clinic_api.DTOS.CreateDto;
using my_clinic_api.Interfaces;
using my_clinic_api.Models;
using my_clinic_api.Services;
using System.Data;

namespace my_clinic_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _repotService;
        private readonly IMapper _mapper;

        public ReportController(IReportService repotService, IMapper mapper)
        {
            _repotService = repotService;
            _mapper = mapper;
        }

        [Authorize(Roles = RoleNames.AdminRole)]

        [HttpGet]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var reports = await _repotService.GetAllAsync();
            if (reports == null) return NotFound();
            return Ok(reports);
        }

        [Authorize(Roles = RoleNames.AdminRole)]

        [HttpGet("GetAllWithData")]
        public async Task<IActionResult> GetAllWithData()
        {
            var reports = await _repotService.GetAllWithData();
            var output = reports.Select(r=> new {
                r.Id,
                Reason = new { r.ReasonId, r.Reason.Reason },
                Description = r.Description,
                Patient = new { r.PatientId, r.Patient.FullName },
                Doctor = new { r.DoctorId, r.Doctor.FullName }
            });
            if (reports == null) return NotFound();
            return Ok(output);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int reportId)
        {
            var report = await _repotService.FindByIdAsync(reportId);
            if (report == null) return NotFound();
            return Ok(report);
        }

        [HttpGet("GetByIdWithData")]
        public async Task<IActionResult> GetByIdWithData(int reportId)
        {
            var report = await _repotService.FindByIdWithData(reportId);
            if (report == null) return NotFound();
            var output = 
                new { report.Id, Reason = new { report.ReasonId , report.Reason.Reason},
                     Description = report.Description,
                     Patient = new { report.PatientId , report.Patient.FullName},
                     Doctor = new { report.DoctorId , report.Doctor.FullName } };
            return Ok(output);

        }

        [Authorize(Roles = RoleNames.PatientRole)]

        [HttpPost]
        public async Task<IActionResult> AddReport([FromForm] CreateReportDto addReportDto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            var report = await _repotService.AddReport(addReportDto);
            if (report == null || report.Id == 0) return BadRequest(ModelState);
            return Ok(report);
        }
    }
}
