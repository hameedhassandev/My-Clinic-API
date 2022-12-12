using my_clinic_api.Models;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace my_clinic_api.Dto.ReturnDtos
{
    public class ReDepartmentDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(120)]
        public string? Name { get; set; }

        [Required]
        [MaxLength(255)]
        public string? Description { get; set; }
        [JsonIgnore]
        public ICollection<Doctor>? doctors { get; set; }
        [JsonIgnore]
        public ICollection<Specialist>? specialists { get; set; }
    }
}
