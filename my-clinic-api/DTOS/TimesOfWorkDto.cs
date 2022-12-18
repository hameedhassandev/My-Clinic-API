using my_clinic_api.Models;
using System.ComponentModel.DataAnnotations;

namespace my_clinic_api.DTOS
{
    public class TimesOfWorkDto
    {
        public string? Id { get; set; }

        [Required]

        public Days day { get; set; }

        [Required]

        public DateTime StartWork { get; set; }

        [Required]
        public DateTime EndWork { get; set; }


        public string? doctorId { get; set; }
    }
}
