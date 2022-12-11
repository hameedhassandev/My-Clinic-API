using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace my_clinic_api.Models.M2M
{
    public class Doctor_Specialist
    {
        public string DoctorId { get; set; }
        public Doctor? doctor { get; set; }


        public int SpecialistId { get; set; }

        public Specialist? Specialist { get; set; }
    }
}
