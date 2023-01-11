using my_clinic_api.Models;

namespace my_clinic_api.DTOS
{
    public class DoctorDropDownDto
    {
        public List<Insurance> Insurances { get; set; }
        public List<Specialist> Specialists { get; set; }
        public List<Hospital> Hospitals { get; set; }
        public List<Area> Areas { get; set; }

        public DoctorDropDownDto()
        {
            Insurances = new List<Insurance>();
            Specialists = new List<Specialist>();
            Hospitals = new List<Hospital>();
            Areas = new List<Area>();
          
        }   
    }
}
