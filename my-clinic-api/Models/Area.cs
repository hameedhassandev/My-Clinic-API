using System.ComponentModel.DataAnnotations;

namespace my_clinic_api.Models
{
    public class Area
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public Cities City { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name ="Area Name")]
        public string ?AreaName { get; set; }



    }
}
