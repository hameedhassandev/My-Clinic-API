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
    public class TimesOfWorkController : ControllerBase
    {
        private readonly ITimesOfWorkService _timesOfWorkService;
        private readonly IMapper _mapper;

        public TimesOfWorkController(ITimesOfWorkService timesOfWorkService, IMapper mapper)
        {
            _timesOfWorkService = timesOfWorkService;
            _mapper = mapper;
        }

        // GET: api/TimesOfWork/GetAll
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {

            var times = await _timesOfWorkService.GetAllAsync();
            if (times == null)
                return NotFound();
            var result = _mapper.Map<IEnumerable<TimesOfWorkDto>>(times);
            return Ok(result);
        }
        // GET: api/TimesOfWork/GetTimesOfDoctor
        [HttpGet("GetTimesOfDoctor")]
        public async Task<IActionResult> GetTimesOfDoctor(string doctorId)
        {

            var times = await _timesOfWorkService.GetTimesOfDoctor(doctorId);
            if (times == null || !times.Any())
                return NotFound();
            var result = _mapper.Map<IEnumerable<TimesOfWorkDto>>(times);
            return Ok(result);
        }

        [HttpPost("AddOrUpdateTimeToDoctor")]
        public async Task<IActionResult> AddTimeToDoctor([FromForm] TimesOfWorkDto dto)
        {
            
            var time = new TimesOfWork
            {
                day = dto.day,
                StartWork = dto.StartWork,
                EndWork = dto.EndWork,
                doctorId = dto.doctorId
            };
            if (!ModelState.IsValid) return BadRequest();
            var result = await _timesOfWorkService.AddAsync(time);
            _timesOfWorkService.CommitChanges();
            return Ok(result);  
        }
    }
}
