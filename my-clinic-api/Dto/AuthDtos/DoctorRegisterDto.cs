using my_clinic_api.Models;
using System.ComponentModel.DataAnnotations;

namespace my_clinic_api.Dto.AuthDtos
{
    public class DoctorRegisterDto
    {

        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(120)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(120)]
        [RegularExpression(@"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)).+$")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [MaxLength(120)]
        public string FullName { get; set; }

        [Required]
        public string DoctorTitle { get; set; }

        [MaxLength(250)]
        public string Bio { get; set; }

        [Required]
        public double Cost { get; set; }

        [Required]
        [Display(Name = "Wating Time in min.")]
        public int WaitingTime { get; set; }

        public int DepartmentId { get; set; }

        [Required]
        public Cities Cities { get; set; }
       

        [Required]
        public int AreaId { get; set; }
        [Required]
        [MaxLength(200)]
        public string Address { get; set; }

        [Required]
        [MaxLength(20)]
        public string PhoneNo { get; set; }

        public byte[]? Image { get; set; }

        public Gender Gender { get; set; }


    }
}
