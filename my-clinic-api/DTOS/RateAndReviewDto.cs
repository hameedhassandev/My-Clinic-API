using my_clinic_api.Models;
using System.ComponentModel.DataAnnotations;

namespace my_clinic_api.DTOS
{
    public class RateAndReviewDto
    {
        public int Id { get; set; }
        [Required]
        public int Rate { get; set; }

        [Required , MaxLength(120)]
        public string? Review { get; set; }

        public PatientDto? Patient { get; set; }
        [Required]
        public string? PatientId { get; set; }
        //public DoctorDto? doctor { get; set; }
        [Required]
        public string? doctorId { get; set; }

        public DateTime CreatedAt { get; set; }
         
    }
}
