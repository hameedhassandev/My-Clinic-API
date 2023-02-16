using System.ComponentModel.DataAnnotations;

namespace my_clinic_api.DTOS.UpdateDro
{
    public class updateDepartmentDto
    {
        [Required]
        public int Id { get; set; }
        [Required]

        public string Name { get; set; }
        [Required]

        public string Description { get; set; }
    }
}
