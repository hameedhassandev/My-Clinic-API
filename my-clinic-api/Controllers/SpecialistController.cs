using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using my_clinic_api.DTOS;
using my_clinic_api.DTOS.CreateDto;
using my_clinic_api.Interfaces;
using my_clinic_api.Models;
using my_clinic_api.Services;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

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

        // GET: api/Specialist/GetAllWithDepartment
        [HttpGet("GetAllWithDepartment")]
        public async Task<IActionResult> GetAllWithDepartment()
        {

            var allWirhDep = await _specialistService.GetAllSpecialistWithDepartment();
            if (allWirhDep == null)
                return NotFound();

            var result = _mapper.Map<List<SpecialistDepartmentDto>>(allWirhDep);

            return Ok(result);
        }

        // GET: api/Specialist/GetAllByDepartmentId/2
        [HttpGet("GetAllByDepartmentId/{departmentId}")]
        public async Task<IActionResult> GetAllByDepartmentId(int departmentId)
        {

            var getAllByDepId = await _specialistService.FindSpecialistByDepartmentId(departmentId);
            if (getAllByDepId == null)
                return NotFound();

            var result = _mapper.Map<List<SpecialistDto>>(getAllByDepId);

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

        [HttpPost("AddSpecialist")]
        public async Task<IActionResult> AddSpecialist([FromForm] CreateSpecialistDto dto)
        {
            if (!ModelState.IsValid) return BadRequest();
            Expression<Func<Specialist, bool>> criteria = d => d.SpecialistName == dto.SpecialistName;
            var check = await _specialistService.FindWithData(criteria);
            if (check is not null) return BadRequest("There ar another specialist in the same name!");
            var specialist = new Specialist
            {
                SpecialistName = dto.SpecialistName,
                departmentId = dto.departmentId,
            };
            var result = await _specialistService.AddAsync(specialist);
            _specialistService.CommitChanges();
            if (result.Id == 0) return NotFound();
            return Ok(result);
        }

        [HttpPost("AddSpecialistToDoctor")]
        public async Task<IActionResult> AddSpecialistToDoctor([FromForm, Required] string doctorId, [FromForm, Required] int specialistId)
        {
            var result = await _specialistService.AddSpecialistToDoctor(doctorId, specialistId);
            if (result)
                return Ok("Specialist Added Successfully");
            return BadRequest();
        }

        [HttpDelete("DeleteSpecialist")]
        public async Task<IActionResult> DeleteSpecialist([FromForm, Required] int specialisId)
        {
            var specialist = await _specialistService.FindByIdAsync(specialisId);
            if (specialist == null) return NotFound("No specialist found in this id!");


            var result = await _specialistService.Delete(specialist);
            _specialistService.CommitChanges();
            if (result.Id == 0) return NotFound();
            return Ok(result);
        }

       

    }
}