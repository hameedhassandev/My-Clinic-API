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

        [HttpPost("AddTimeToDoctor")]
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

        [HttpPut("UpdateTimeOfDoctor")]
        public async Task<IActionResult> UpdateTimeOfDoctor(int TimeId, [FromBody] TimesOfWorkDto dto)
        {
            var checkIsExist = await _timesOfWorkService.GetByIdAsync(TimeId);
            if (checkIsExist == null) return BadRequest();
            else if (checkIsExist.doctorId != dto.doctorId) return BadRequest("This id for another doctor!");
            else if (checkIsExist.day != dto.day) return BadRequest("This day is incorrect");
            else if (checkIsExist.day == dto.day && checkIsExist.StartWork == dto.StartWork && checkIsExist.EndWork == dto.EndWork) return BadRequest("There are no changes found");
            var time = new TimesOfWork
            {
                Id = TimeId,
                day = dto.day,
                StartWork = dto.StartWork,
                EndWork = dto.EndWork,
                doctorId = dto.doctorId
            };
            if (!ModelState.IsValid) return BadRequest();
            var result = _timesOfWorkService.Update(time);
            _timesOfWorkService.CommitChanges();
            return Ok(result);

        }
        [HttpDelete("DeleteTimeOfDoctor")]
        public async Task<IActionResult> DeleteTimeOfDoctor([FromForm] int TimeIdo)
        {
            var time = await _timesOfWorkService.GetByIdAsync(TimeIdo);
            if (time == null) return BadRequest("No time was found");
            var result = _timesOfWorkService.Delete(time);
            _timesOfWorkService.CommitChanges();
            return Ok(result);
        }
    }
}
