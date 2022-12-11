namespace my_clinic_api.Models.M2M
{
    public class Doctor_Insurance
    {
        public string DoctorId { get; set; }
        public Doctor? doctor { get; set; }


        public int InsuranceId { get; set; }

        public Insurance? Insurance { get; set; }
    }
}
