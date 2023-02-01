using my_clinic_api.Models;
using System.ComponentModel.DataAnnotations;

namespace my_clinic_api.DTOS
{
    public class TimesOfWorkDto
    {
        public int Id { get; set; }

        [Required]

        public Days day { get; set; }

        [Required]

        public TimeSpan StartWork { get; set; }

        [Required]
        public TimeSpan EndWork { get; set; }

        //public Doctor Doctor { get; set; }
        [Required]
        public string? doctorId { get; set; }
    }
}
