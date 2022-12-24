using my_clinic_api.DTOS;
using my_clinic_api.Models;

namespace my_clinic_api.Interfaces
{
    public interface IRateandReviewService : IBaseRepository<RateAndReview>
    {
        Task<RateAndReview> FindReviewByIdWithData(int reviewId);
        Task<IEnumerable<RateAndReview>> GetReviewsOfDoctor(string doctorId);
        Task<IEnumerable<RateAndReview>> GetReviewsOfPatient(string patientId);
        Task<RateAndReview> AndRateAndReview(RateAndReviewDto rateAndReviewDto);
    }
}
