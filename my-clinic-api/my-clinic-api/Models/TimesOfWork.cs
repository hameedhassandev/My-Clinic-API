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

        public DateTime StartWork { get; set; }

        [Required]
        public DateTime EndWork { get; set; }

        [Required]
        public int DoctorId { get; set; }

        //public Doctor doctor { get; set; }
    }
}
