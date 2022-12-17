using my_clinic_api.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace my_clinic_api.DTOS
{
    public class PatientDto
    {
        public string? Id { get; set; }
        [Required]
        [MaxLength(120)]
        public string? FullName { get; set; }

        [Required]
        public Cities? Cities { get; set; }

        [Required]
        public int AreaId { get; set; }
        public Area? Area { get; set; }

        public string? Address { get; set; }


        public string? PhoneNo { get; set; }

        public byte[]? Image { get; set; }

        public Gender? Gender { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        [DefaultValue(true)]
        public bool IsActive { get; set; }
        public ICollection<RateAndReviewDto> RateAndReviews { get; set; }
    }
}
