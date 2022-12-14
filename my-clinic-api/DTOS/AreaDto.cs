using my_clinic_api.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace my_clinic_api.DTOS
{
    public class AreaDto
    {
        [Required]
        public Cities City { get; set; }

        [Required]
        [MaxLength(100)]
        public string? AreaName { get; set; }
    }
}
