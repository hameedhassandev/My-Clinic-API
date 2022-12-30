using System.ComponentModel.DataAnnotations;

namespace my_clinic_api.DTOS.CreateDto
{
    public class CreateReportDto
    {
        [Required, MaxLength(200)]
        public string? Description { get; set; }

        [Required]
        public int ReasonId { get; set; }

        [Required]
        public string? DoctorId { get; set; }

        [Required]
        public string? PatientId { get; set; }
    }
}
