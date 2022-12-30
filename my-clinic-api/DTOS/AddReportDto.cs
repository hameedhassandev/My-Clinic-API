using my_clinic_api.Models;
using System.ComponentModel.DataAnnotations;

namespace my_clinic_api.DTOS
{
    public class AddReportDto
    {

        [Required, MaxLength(200)]
        public string? Description { get; set; }

        [Required]
        public int ReasonId { get; set; }

        [Required]
        public string? DoctorId { get; set; }

        [Required]
        public string? PatientId { get; set; }

        public static explicit operator AddReportDto(ReportDto report)
        {
            return new AddReportDto
            {
                Description = report.Description,
                ReasonId = report.ReasonId,
                DoctorId = report.DoctorId,
                PatientId = report.PatientId
            };
        }
    }
}
