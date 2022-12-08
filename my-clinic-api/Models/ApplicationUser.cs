using Microsoft.AspNetCore.Identity;
using my_clinic_api.Models.RefreshTokens;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace my_clinic_api.Models
{
    public class ApplicationUser : IdentityUser
    {
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

        //refresh token NP
        public List<RefreshToken>? RefreshToken { get; set; }

    }
}
