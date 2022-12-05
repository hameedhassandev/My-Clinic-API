namespace my_clinic_api.Models
{
    public class AuthModel
    {
        public string Massage { get; set; }
        public bool IsAuth { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public DateTime ExpiresOn { get; set; }



    }
}
