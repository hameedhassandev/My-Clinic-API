using System.ComponentModel.DataAnnotations;

namespace my_clinic_api.DTOS.AuthDtos
{
    public class confirmMailDto
    {
        [Required]
        public string Token { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
