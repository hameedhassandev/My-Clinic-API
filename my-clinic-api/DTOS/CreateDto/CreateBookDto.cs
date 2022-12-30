using System.ComponentModel.DataAnnotations;

namespace my_clinic_api.DTOS.CreateDto
{
    public class CreateBookDto
    {
        [Required]
        [DisplayFormat(DataFormatString = "{dddd/dd/MMMM/yyyy}")]
        public DateTime Time { get; set; }

        [Required]
        public string? DoctorId { get; set; }
        [Required]
        public string? PatientId { get; set; }
    }
}
