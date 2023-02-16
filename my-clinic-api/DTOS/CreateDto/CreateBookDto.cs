using my_clinic_api.Models;
using System.ComponentModel.DataAnnotations;

namespace my_clinic_api.DTOS.CreateDto
{
    public class CreateBookDto
    {
        [Required]
        public Days Day { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{dddd/dd/MMMM/yyyy}")]
        public TimeSpan Time { get; set; }

        [Required]
        public string? DoctorId { get; set; }
        [Required]
        public string? PatientId { get; set; }
    }
}
