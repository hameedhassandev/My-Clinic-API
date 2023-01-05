using my_clinic_api.Models;
using System.ComponentModel.DataAnnotations;

namespace my_clinic_api.DTOS
{
    public class ReportDto
    {
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string? Description { get; set; }

        public ReportReasonsDto? Reason { get; set; }

        [Required]
        public int ReasonId { get; set; }
        public DoctorDto? Doctor { get; set; }

        [Required]
        public string? DoctorId { get; set; }
        public PatientDto? Patient { get; set; }

        [Required]
        public string? PatientId { get; set; }

        public static explicit operator ReportDto(AddReportDto report)
        {
            return new ReportDto
            {
                Description = report.Description,
                ReasonId = report.ReasonId,
                DoctorId = report.DoctorId,
                PatientId = report.PatientId
            };
        }

    }
}
