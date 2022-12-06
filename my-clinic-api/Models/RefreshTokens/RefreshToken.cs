using Microsoft.EntityFrameworkCore;

namespace my_clinic_api.Models.RefreshTokens
{
    [Owned]
    public class RefreshToken
    {
        public string? Token { get; set; }
        public DateTime ExpiresOn { get; set; }
        public bool IsExpired => DateTime.UtcNow >= ExpiresOn; 
        public DateTime CreatedOn { get; set; }
        public DateTime RevokedOn { get; set; }
        public bool TokenIsActive => RevokedOn == null && !IsExpired;
        


    }
}
