using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace my_clinic_api.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(120)]
        [Display(Name = "Full Name")]
        public string? FullName { get; set; }

        [Required]
        public Cities? Cities { get; set; }

        [Required]
        public Area? Area { get; set; }

        public string? Address { get; set; }

        [Display(Name ="Phone Number")]
        public string? PhoneNo { get; set; }

        public byte[]? Image { get; set; }

        public Gender? Gender { get; set; }


    }
}
