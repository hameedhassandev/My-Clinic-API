namespace my_clinic_api.Models
{
    public class Patient : ApplicationUser
    {

        public ICollection<RateAndReview> RateAndReviews { get; set; }  
    }
}
