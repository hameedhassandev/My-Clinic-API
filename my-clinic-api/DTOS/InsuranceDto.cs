using System.ComponentModel.DataAnnotations;

namespace my_clinic_api.DTOS
{
    public class InsuranceDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string? CompanyName { get; set; }

        public double Discount { get; set; }
    }
}
