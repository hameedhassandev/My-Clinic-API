using System.ComponentModel.DataAnnotations;

namespace my_clinic_api.DTOS
{
    public class updateImageDto
    {
        [Required]
        public string? userId { get; set; }

        [Required]
        public IFormFile? Image { get; set; }

    }
}
