using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace my_clinic_api.Models
{
    public class Book
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{dddd/dd/MMMM/yyyy}")]
        public DateTime Time { get; set; }

        public DateTime? CreatedAt { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{dddd/dd/MMMM/yyyy}")]
        public DateTime ExpiryDate { get; set; }
        public bool IsConfirmed { get; set; } = false;

        [Required]
        public Doctor? Doctor { get; set; }
        public string? DoctorId { get; set; }
        [Required]
        public Patient? Patient { get; set; }
        public string? PatientId { get; set; }
    }
}
