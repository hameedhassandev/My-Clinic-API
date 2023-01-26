using my_clinic_api.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace my_clinic_api.Dto.AuthDtos
{
    public class UserRegisterDto
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
        public Cities Cities { get; set; }
        /* [Required]
         public Area Area { get; set; }*/

        [Required]
        public int AreaId { get; set; }
        [Required]
        [MaxLength(200)]
        public string  Address { get; set; }

        [Required]
        [MaxLength(20)]
        public string  PhoneNo { get; set; }

        public IFormFile? Image { get; set; }

        public Gender  Gender { get; set; }

        

      

    }
}
