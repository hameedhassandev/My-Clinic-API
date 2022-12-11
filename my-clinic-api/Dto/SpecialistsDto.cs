using my_clinic_api.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace my_clinic_api.Dto
{
    public class SpecialistsDto
    {
        [Required]
        public int Id { get; set; }


        [Required]
        [MaxLength(120)]
        public string? SpecialistName { get; set; }
        [JsonIgnore]
        public Department? department { get; set; }
        //[JsonIgnore]
        public ICollection<Doctor>? Doctores { get; set; }

    }
}
