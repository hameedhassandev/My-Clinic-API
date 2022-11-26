using System.ComponentModel.DataAnnotations;

namespace my_clinic_api.Models
{
    public class Specialist
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        [Required , MaxLength(120)]
        public string SpecialistName { get; set; }

        public Department department { get; set; }
    }
}
