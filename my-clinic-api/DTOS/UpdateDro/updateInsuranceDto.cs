using System.ComponentModel.DataAnnotations;

namespace my_clinic_api.DTOS.UpdateDro
{
    public class updateInsuranceDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string? CompanyName { get; set; }

        [Required]
        public double Discount { get; set; }
    }
}
