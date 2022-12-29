using System.ComponentModel.DataAnnotations;

namespace my_clinic_api.Models
{
    public class RateAndReview
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int Rate { get; set; }

        [MaxLength(120)]
        public string ?Review { get; set; }

        public Patient? Patient { get; set; }
        public string? PatientId { get; set; }
        public Doctor? doctor { get; set; }
        public string? doctorId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
