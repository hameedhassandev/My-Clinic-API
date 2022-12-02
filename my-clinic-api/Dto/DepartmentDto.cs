using System.ComponentModel.DataAnnotations;

namespace my_clinic_api.Dto
{
    public class DepartmentDto
    {
        [Required]
        [MaxLength(120)]
        public string? Name { get; set; }

        [Required]
        [MaxLength(255)]
        public string? Description { get; set; }
    }
}
