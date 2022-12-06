using Newtonsoft.Json;

namespace my_clinic_api.Dto.AuthDtos
{
    public class AuthModelDto
    {
        public string? Massage { get; set; }
        public bool IsAuth { get; set; }
        public string ?Email { get; set; }
        public string? Token { get; set; }
        public List<string>? Roles { get; set; }

        // to ignor some prop.
        [JsonIgnore]
        public string? RefreshToke { get; set; }

        public DateTime RefreshTokenExpiration { get; set; }

    }
}
