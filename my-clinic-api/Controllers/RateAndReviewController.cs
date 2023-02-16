using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_clinic_api.Classes;
using my_clinic_api.DTOS;
using my_clinic_api.DTOS.CreateDto;
using my_clinic_api.Interfaces;
using my_clinic_api.Models;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace my_clinic_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RateAndReviewController : ControllerBase
    {
        private readonly IRateandReviewService _rateandreviewService;
        private readonly IMapper _mapper;

        public RateAndReviewController(IRateandReviewService rateandreviewService, IMapper mapper)
        {
            _rateandreviewService = rateandreviewService;
            _mapper = mapper;
        }

        [Authorize(Roles = RoleNames.AdminRole)]

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var data = await _rateandreviewService.GetAllAsync();
            if (data == null) return NotFound("No Data are found");
            return Ok(data);
        }

        [HttpGet("GetAllWithData")]
        public async Task<IActionResult> GetAllWithData()
        {
            var data = await _rateandreviewService.GetAllWithData();
            if (data == null) return NotFound("No Data are found");
            var output = _mapper.Map<IEnumerable<RateAndReviewDto>>(data);
            return Ok(output);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int rateAndReviewId)
        {
            var rateAndReview = await _rateandreviewService.FindByIdAsync(rateAndReviewId);
            if (rateAndReview == null) return NotFound("No Review Found with this id!");
            return Ok(rateAndReview);
        }

        [HttpGet("GetByIdWithData")]
        public async Task<IActionResult> GetByIdWithData(int rateAndReviewId)
        {
            var rateAndReview = await _rateandreviewService.FindReviewByIdWithData(rateAndReviewId);
            if (rateAndReview == null) return NotFound("No Review Found with this id!");
            var output = _mapper.Map<RateAndReviewDto>(rateAndReview);
            return Ok(output);
        }

        [Authorize(Roles = RoleNames.DoctorRole)]

        [HttpGet("GetReviewsOfDoctor")]
        public async Task<IActionResult> GetReviewsOfDoctor(string doctorId)
        {
            var rateAndReviews = await _rateandreviewService.GetReviewsOfDoctor(doctorId);
            if (rateAndReviews == null) return NotFound("No Reviews were found");
            var output = _mapper.Map<IEnumerable<RateAndReviewDto>>(rateAndReviews);
            return Ok(output);
        }

        [Authorize(Roles = RoleNames.PatientRole)]

        [HttpGet("GetReviewsOfPatient")]
        public async Task<IActionResult> GetReviewsOfPatient(string patientId)
        {
            var rateAndReviews = await _rateandreviewService.GetReviewsOfPatient(patientId);
            if (rateAndReviews == null) return NotFound("No Reviews were found");
            var output = _mapper.Map<IEnumerable<RateAndReviewDto>>(rateAndReviews);
            return Ok(output);
        }

        [Authorize(Roles = RoleNames.PatientRole)]

        [HttpPost("AddRateAndReview")]
        public async Task<IActionResult> AddRateAndReview([FromForm] CreateRateAndReviewDto rateAndReviewDto)
        {
            if (rateAndReviewDto == null) return BadRequest("RateAndReview is required");
            else if (!ModelState.IsValid) return BadRequest("RateAndReview is invalid");
            var result = await _rateandreviewService.AndRateAndReview(rateAndReviewDto);
            if (result == null) return NotFound("Failed add Review");
            var output = _mapper.Map<RateAndReviewDto>(result);
            return Ok(output);
        }

        [Authorize(Roles = RoleNames.AdminRole)]

        [HttpDelete("DeleteRateAndReview")]
        public async Task<IActionResult> DeleteRateAndReview ([FromForm, Required] int reviewId , [FromForm, Required] string patientId)
        {
            var review = await _rateandreviewService.FindByIdAsync(reviewId);
            if (review is not null)
                if (review.PatientId == patientId)
                {
                    var result = await _rateandreviewService.Delete(review);
                    _rateandreviewService.CommitChanges();
                    if (result == null) return NotFound();
                    return Ok(result);
                }
            return NotFound();
        }
    }
}
