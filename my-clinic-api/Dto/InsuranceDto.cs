using System.ComponentModel.DataAnnotations;

namespace my_clinic_api.Dto
{
    public class InsuranceDto
    {
        [Required]
        [MaxLength(100)]
        public string? CompanyName { get; set; }

        public double Discount { get; set; }
    }
}
