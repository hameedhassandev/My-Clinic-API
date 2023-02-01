using System.ComponentModel.DataAnnotations;

namespace my_clinic_api.Models
{
    public class TimesOfWork
    {
        [Required]
        public int Id { get; set; }


        [Required]

        public Days day { get; set; }

        [Required]

        public TimeSpan StartWork { get; set; }

        [Required]
        public TimeSpan EndWork { get; set; }


        public Doctor? doctor { get; set; }
        public string? doctorId { get; set; }
    }
}
