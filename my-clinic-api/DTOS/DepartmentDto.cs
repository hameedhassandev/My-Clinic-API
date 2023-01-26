using my_clinic_api.Models;
using System.ComponentModel.DataAnnotations;

namespace my_clinic_api.DTOS
{
    public class DepartmentDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(120)]
        public string? Name { get; set; }

        [Required]
        [MaxLength(255)]

        public string? Description { get; set; }

        public ICollection<DoctorDto>? doctors { get; set; }

        public ICollection<SpecialistDto>? specialists { get; set; }

        //public static explicit operator DepartmentDto(Department department)
        //{
        //    return new DepartmentDto
        //    {
        //        Id = department.Id,
        //        Name = department.Name,
        //        Description = department.Description,
        //    };
        //}
    }
}
