namespace my_clinic_api.Models.M2M
{
    public class Doctor_Hospital
    {
        public string DoctorId { get; set; }
        public Doctor? doctor { get; set; }


        public int HospitalId { get; set; }

        public Hospital? Hospital { get; set; }
    }
}
