using System.ComponentModel.DataAnnotations;

namespace my_clinic_api.DTOS.CreateDto
{
    public class CreateInsuranceDto
    {
        [Required]
        [MaxLength(100)]
        public string? CompanyName { get; set; }

        public double Discount { get; set; }
    }
}
