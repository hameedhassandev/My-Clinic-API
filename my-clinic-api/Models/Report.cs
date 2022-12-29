using System.ComponentModel.DataAnnotations;

namespace my_clinic_api.Models
{
    public class Report
    {
        [Required]
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string? Description { get; set; }

        public ReportReasons? Reason { get; set; }

        [Required]
        public int ReasonId { get; set; }
        public Doctor? Doctor { get; set; }

        [Required]
        public string? DoctorId { get; set; }
        public Patient? Patient { get; set; }

        [Required]
        public string? PatientId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;


    }
}
