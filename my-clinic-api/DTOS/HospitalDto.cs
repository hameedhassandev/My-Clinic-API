using System.ComponentModel.DataAnnotations;

namespace my_clinic_api.DTOS
{
    public class HospitalDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string? Name { get; set; }

        [Required]
        [MaxLength(255)]
        public string? Address { get; set; }
    }
}
