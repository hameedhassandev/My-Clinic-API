using System.ComponentModel.DataAnnotations;

namespace my_clinic_api.Models
{
    public class Insurance
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string CompanyName { get; set; }

        public int Discount { get; set; }
       
       
    }
}
