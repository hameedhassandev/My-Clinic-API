using System.ComponentModel.DataAnnotations;

namespace my_clinic_api.Models
{
    public class Hospital
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string ?Name { get; set; }

        [Required]
        [MaxLength(255)]
        public string ?Address { get; set; }

        public virtual ICollection<Doctor>? doctors { get; set; }


    }
}
