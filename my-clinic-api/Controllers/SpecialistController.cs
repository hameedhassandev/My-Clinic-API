using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using my_clinic_api.Dto;
using my_clinic_api.Interfaces;
using my_clinic_api.Models;
using my_clinic_api.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace my_clinic_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialistController : ControllerBase
    {
        private readonly ISpecialistService _specialistService;
        private readonly IMapper _mapper;
        public SpecialistController(ISpecialistService specialistService, IMapper mapper)
        {
            _specialistService = specialistService;
            _mapper = mapper;
        }

        // GET: api/Specialist/GetAll
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {

            var result = await _specialistService.GetAllAsync();
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // GET: api/Specialist/GetAllWithDoctors
        [HttpGet("GetAllWithDoctors")]
        public async Task<IActionResult> GetAllWithDoctors()
        {
            var result = await _specialistService.GetAllWithIncludeAsync(new List<string>() { "Doctores" });
            if (result == null)
                return NotFound();
            var output = _mapper.Map<IEnumerable<SpecialistsDto>>(result);

            return Ok(output);
        }
    }
}