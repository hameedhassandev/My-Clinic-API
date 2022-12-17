using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_clinic_api.DTOS;
using my_clinic_api.Interfaces;
using my_clinic_api.Models;
using my_clinic_api.Services;

namespace my_clinic_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patienService;
        private readonly IMapper _mapper;

        public PatientController(IPatientService patienService, IMapper mapper)
        {
            _patienService = patienService;
            _mapper = mapper;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var patients = await _patienService.GetAllAsync();
            if (patients == null)
                return NotFound();
            var result =  _mapper.Map<IEnumerable<PatientDto>>(patients);
            return Ok(result);
        }

        [HttpGet("GetAllWithReviews")]
        public async Task<IActionResult> GetAllWithReviews()
        {
            var patients = await _patienService.GetAllWithIncludeAsync(new List<string> { "RateAndReviews" });
            if (patients == null)
                return NotFound();
            var result = _mapper.Map<IEnumerable<PatientDto>>(patients);
            return Ok(result);
        }
    }
}
