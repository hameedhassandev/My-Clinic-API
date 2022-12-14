using my_clinic_api.Models;
using System.ComponentModel.DataAnnotations;

namespace my_clinic_api.DTOS
{
    public class RateAndReviewDto
    {
        public int Id { get; set; }

        [Required]
        public int Rate { get; set; }

        [MaxLength(120)]
        public string? Review { get; set; }
        public PatientReviewDto? User { get; set; }
        //public Doctor? doctor { get; set; }
        //public string? DoctorId { get; set; }
    }
}
