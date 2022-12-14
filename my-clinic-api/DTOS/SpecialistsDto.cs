using my_clinic_api.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace my_clinic_api.DTOS
{
    public class SpecialistsDto
    {
        [Required]
        public int Id { get; set; }


        [Required]
        [MaxLength(120)]
        public string? SpecialistName { get; set; }


    }
}