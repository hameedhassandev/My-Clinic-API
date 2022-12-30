using System.ComponentModel.DataAnnotations;

namespace my_clinic_api.DTOS.CreateDto
{
    public class CreateHospitalDto
    {
        [Required]
        [MaxLength(200)]
        public string? Name { get; set; }

        [Required]
        [MaxLength(255)]
        public string? Address { get; set; }
    }
}
