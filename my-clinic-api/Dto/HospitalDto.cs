using System.ComponentModel.DataAnnotations;

namespace my_clinic_api.Dto
{
    public class HospitalDto
    {

        [Required]
        [MaxLength(200)]
        public string ?HospitalName { get; set; }

        [Required]
        [MaxLength(255)]
        public string? HospitalAddress { get; set; }
    }
}
