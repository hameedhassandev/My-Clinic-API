using my_clinic_api.Models;
using System.ComponentModel.DataAnnotations;

namespace my_clinic_api.DTOS.CreateDto
{
    public class CreateSpecialistDto
    {
        [Required]
        [MaxLength(120)]
        public string? SpecialistName { get; set; }
        [Required]
        public int departmentId { get; set; }
    }
}
