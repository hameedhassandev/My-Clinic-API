using System.ComponentModel.DataAnnotations;

namespace my_clinic_api.DTOS.UpdateDro
{
    public class updateSpecialistDto
    {
        [Required]

        public int Id { get; set; }
        [Required]

        public string? SpecialistName { get; set; }
        [Required]
        public int departmentId { get; set; }
    }
}
