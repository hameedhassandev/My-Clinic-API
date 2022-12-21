using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using my_clinic_api.DTOS;
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

        // GET: api/Specialist/GetAllWithData
        [HttpGet("GetAllWithData")]
        public async Task<IActionResult> GetAllWithData()
        {
            var result = await _specialistService.GetAllWithData();
            if (result == null)
                return NotFound();
            var output = _mapper.Map<IEnumerable<SpecialistDto>>(result);

            return Ok(output);
        }

        // GET: api/Specialist/GetById/5
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {

            var result = await _specialistService.FindByIdAsync(id);
            if (result == null)
                return NotFound();
            var output = _mapper.Map<SpecialistDto>(result);
            return Ok(output);
        }
        // GET: api/Specialist/GetByIdWithData/5
        [HttpGet("GetByIdWithData/{id}")]
        public async Task<IActionResult> GetByIdWithData(int id)
        {

            var result = await _specialistService.FindSpecialistByIdWithData(id);
            if (result == null)
                return NotFound();
            var output = _mapper.Map<SpecialistDto>(result);
            return Ok(output);
        }

    }
}