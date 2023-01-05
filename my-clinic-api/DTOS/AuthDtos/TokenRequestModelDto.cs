using System.ComponentModel.DataAnnotations;

namespace my_clinic_api.DTOS.AuthDtos
{
    public class TokenRequestModelDto
    {
        [Required]
        [MaxLength(120)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(120)]
        public string Password { get; set; }
    }
}
