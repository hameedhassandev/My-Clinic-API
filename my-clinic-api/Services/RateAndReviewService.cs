using AutoMapper;
using my_clinic_api.DTOS;
using my_clinic_api.DTOS.CreateDto;
using my_clinic_api.Interfaces;
using my_clinic_api.Models;
using System.Linq.Expressions;

namespace my_clinic_api.Services
{
    public class RateandReviewService : BaseRepository<RateAndReview>, IRateandReviewService
    {
        public RateandReviewService(ApplicationDbContext Context) : base(Context)
        {
            
        }
        public async Task<RateAndReview> FindReviewByIdWithData(int reviewId)
        {
            Expression<Func<RateAndReview, bool>> criteria = d => d.Id == reviewId;
            var review = await FindWithData(criteria);
            return review;
        }

        public async Task<IEnumerable<RateAndReview>> GetReviewsOfDoctor(string doctorId)
        {
            Expression<Func<RateAndReview, bool>> criteria = r => r.doctorId == doctorId;
            var reviews = await FindAllWithData(criteria);
            return reviews;
        }
        
        public async Task<IEnumerable<RateAndReview>> GetReviewsOfPatient(string patientId)
        {
            Expression<Func<RateAndReview, bool>> criteria = r => r.PatientId == patientId;
            var reviews = await FindAllWithData(criteria);
            return reviews;
        }

        public async Task<RateAndReview> AndRateAndReview(CreateRateAndReviewDto rateAndReviewDto)
        {
            var review = new RateAndReview
            {
                Rate = rateAndReviewDto.Rate,
                Review = rateAndReviewDto.Review,
                PatientId = rateAndReviewDto.PatientId,
                doctorId = rateAndReviewDto.doctorId
            };
            var result = await AddAsync(review);
            CommitChanges();
            return result;
        }
    }
}
