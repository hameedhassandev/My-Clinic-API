using Newtonsoft.Json;

namespace my_clinic_api.Dto.AuthDtos
{
    public class AuthModelDto
    {
        public string? Massage { get; set; }
        public string? Id { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public bool IsAuth { get; set; }
        public List<string>? Roles { get; set; }
        public string? Token { get; set; }
        public DateTime? ExpiresOn { get; set; }
        // to ignor some prop.
        [JsonIgnore]
        public string? RefreshToken { get; set; }

        public DateTime RefreshTokenExpiration { get; set; }


    }
}
