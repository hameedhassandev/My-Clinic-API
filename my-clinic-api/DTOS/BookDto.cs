using my_clinic_api.Models;
using System.ComponentModel.DataAnnotations;

namespace my_clinic_api.DTOS
{
    public class BookDto
    {
        public int Id { get; set; } = 0;

        [Required]
        [DisplayFormat(DataFormatString = "{dddd/dd/MMMM/yyyy}")]
        public DateTime Time { get; set; }

        public bool IsConfirmed { get; set; } = false;

        [Required]
        public string? DoctorId { get; set; }
        [Required]
        public string? PatientId { get; set; }
    }
}
