using System.ComponentModel.DataAnnotations;

namespace my_clinic_api.DTOS.AuthDtos
{
    public class updateDoctorDto
    {
        [Required]
        public string doctorId { get; set; }

        [Required]
        public string DoctorTitle { get; set; }

        [Required]
        public double Cost { get; set; }

        [Required]
        public int WaitingTime { get; set; }

        public string PhoneNo { get; set; }
      /*  public List<int> SpecialistsIds { get; set; }
        public List<int> HospitalsIds { get; set; }
        public List<int> InsuranceIds { get; set; }*/
    }
}
