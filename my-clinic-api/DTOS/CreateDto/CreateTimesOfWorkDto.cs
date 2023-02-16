using my_clinic_api.Models;
using System.ComponentModel.DataAnnotations;

namespace my_clinic_api.DTOS.CreateDto
{
    public class CreateTimesOfWorkDto
    {
        [Required]

        public Days day { get; set; }

        [Required]

        public TimeSpan StartWork { get; set; }

        [Required]
        public TimeSpan EndWork { get; set; }

        [Required]
        public string doctorId { get; set; }
    }
}
