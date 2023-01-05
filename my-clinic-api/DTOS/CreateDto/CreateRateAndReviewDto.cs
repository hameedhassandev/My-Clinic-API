using System.ComponentModel.DataAnnotations;

namespace my_clinic_api.DTOS.CreateDto
{
    public class CreateRateAndReviewDto
    {
        [Required]
        public int Rate { get; set; }

        [Required, MaxLength(120)]
        public string? Review { get; set; }

        [Required]
        public string? PatientId { get; set; }
        [Required]
        public string? doctorId { get; set; }
    }
}
