using my_clinic_api.Models;
using System.ComponentModel.DataAnnotations;

namespace my_clinic_api.DTOS.UpdateDro
{
    public class updateAreaDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string AreaName { get; set; }
        [Required]
        public Cities City { get; set; }
    }
}
