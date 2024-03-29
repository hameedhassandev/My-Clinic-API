using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_clinic_api.Classes;
using my_clinic_api.DTOS;
using my_clinic_api.DTOS.CreateDto;
using my_clinic_api.Interfaces;
using my_clinic_api.Models;
using my_clinic_api.Services;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace my_clinic_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimesOfWorkController : ControllerBase
    {
        private readonly ITimesOfWorkService _timesOfWorkService;
        private readonly IBookService _bookService;
        private readonly IDoctorService _doctorService;
        private readonly IMapper _mapper;

        public TimesOfWorkController(ITimesOfWorkService timesOfWorkService, IMapper mapper, IBookService bookService, IDoctorService doctorService)
        {
            _timesOfWorkService = timesOfWorkService;
            _mapper = mapper;
            _bookService = bookService;
            _doctorService = doctorService;
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

        // GET: api/TimesOfWork/GetAllWithData
        [HttpGet("GetAllWithData")]
        public async Task<IActionResult> GetAllWithData()
        {
            var result = await _timesOfWorkService.GetAllWithData();
            if (result == null)
                return NotFound();
            var output = _mapper.Map<IEnumerable<TimesOfWorkDto>>(result);

            return Ok(output);
        }

        // GET: api/TimesOfWork/GetById/5
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {

            var result = await _timesOfWorkService.FindByIdAsync(id);
            if (result == null)
                return NotFound();
            var output = _mapper.Map<TimesOfWorkDto>(result);
            return Ok(output);
        }
        // GET: api/TimesOfWork/GetByIdWithData/5
        [HttpGet("GetByIdWithData/{id}")]
        public async Task<IActionResult> GetByIdWithData(int id)
        {

            var result = await _timesOfWorkService.FindTimeByIdWithData(id);
            if (result == null)
                return NotFound();
            var output = _mapper.Map<TimesOfWorkDto>(result);
            return Ok(output);
        }
        // GET: api/TimesOfWork/GetDatesOfDoctor
        [HttpGet("GetDatesOfDoctor")]
        public async Task<IActionResult> GetDatesOfDoctor(string doctorId)
        {

            var times = await _timesOfWorkService.GetTimesOfDoctor(doctorId);
            if (times == null || !times.Any())
                return NotFound("Doctor has no times!");
            var result = _mapper.Map<IEnumerable<TimesOfWorkDto>>(times);
            return Ok(result);
        }

        [HttpGet("GetAllTimesOfDoctor")]
        public async Task<IActionResult> GetAllTimesOfDoctor(string doctorId)
        {

            var times = await _timesOfWorkService.GetTimesOfDoctor(doctorId);
            if (!times.Any()) return NotFound("Doctor has no times! ");
            var bookings = await _bookService.GetBookingsOfDoctor(doctorId);
            // Filter bookings to remove expierd 
            bookings = bookings.Where(b =>
                b.ExpiryDate.Date > DateTime.Now.Date);
            Dictionary<string, Dictionary<TimeSpan , bool>> days = new Dictionary<string, Dictionary<TimeSpan ,bool>>();
            //if (!bookings.Any()) return BadRequest("No available times are found");

            var waitingTime = await _doctorService.GetWaitingTimeOfDoctor(doctorId);
            var i = TimeSpan.Zero;
            foreach (var time in times)
            {
                i = time.StartWork;
                Dictionary<TimeSpan, bool> availables = new Dictionary<TimeSpan, bool>();
                while (i <= time.EndWork)
                {
                    if (!bookings.Any(t => t.Time.TimeOfDay == i))
                    {
                        availables.Add(i , true);
                    }
                    else
                    {
                        availables.Add(i , false);
                    }
                    var timeToAdd = TimeSpan.FromMinutes(waitingTime);
                    i = i.Add(timeToAdd);
                }
                days.Add(time.day.ToString(), availables);

            }

            if (days == null || !days.Any())
                return NotFound();
            return Ok(days);
        }


        [HttpGet("GetAvailableTimesOfDoctorNext3Days")]
        public async Task<IActionResult> GetAvailableTimesOfDoctorNext3Days(string doctorId)
        {

            var times = await _timesOfWorkService.GetTimesOfDoctor(doctorId);
            if (!times.Any()) return NotFound("Doctor has no times! ");
            // Filter times to get today and the next 2 days only
           /* times = times.Where(t => t.day.ToString() == _timesOfWorkService.GetNextDaysFromNow(0) ||
                t.day.ToString() == _timesOfWorkService.GetNextDaysFromNow(1) ||
                t.day.ToString() == _timesOfWorkService.GetNextDaysFromNow(2)
            );*/
            var bookings = await _bookService.GetBookingsOfDoctor(doctorId);
            // Filter bookings to remove expierd 
            bookings = bookings.Where(b=> 
                b.ExpiryDate.Date > DateTime.Now.Date);
            Dictionary<string, IList<TimeSpan>> days = new Dictionary<string, IList<TimeSpan>>();
            //if (!bookings.Any()) return BadRequest("No available times are found");

            var waitingTime = await _doctorService.GetWaitingTimeOfDoctor(doctorId);
            var i = TimeSpan.Zero;
            foreach (var time in times)
            {
                i = time.StartWork;
                IList<TimeSpan> availables = new List<TimeSpan>();
                while (i <= time.EndWork)
                {
                    if (!bookings.Any(t=>t.Time.TimeOfDay == i) )
                    {
                        availables.Add(i);
                    }
                    var timeToAdd = TimeSpan.FromMinutes(waitingTime);
                    i = i.Add(timeToAdd);
                }
                days.Add(time.day.ToString(),availables);
                    
            }
                
            if (days == null || !days.Any())
                return NotFound();
            return Ok(days);
        }

        [Authorize(Roles = RoleNames.DoctorRole)]
        [HttpPost("AddTimeToDoctor")]
        public async Task<IActionResult> AddTimeToDoctor([FromBody] CreateTimesOfWorkDto dto)
        {
            var add = await _timesOfWorkService.AddTimetoDoctor(dto);
            /// Return problem same as AddBook Function 
            if(add != "Success") return BadRequest(add);
            return Ok(dto);
        }

        [Authorize(Roles = RoleNames.DoctorRole)]

        [HttpPut("UpdateTimeOfDoctor")]
        public async Task<IActionResult> UpdateTimeOfDoctor([FromForm, Required] int TimeId, [FromForm] CreateTimesOfWorkDto dto)
        {
            var checkIsExist = await _timesOfWorkService.FindByIdAsync(TimeId);
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
            var result = await _timesOfWorkService.Update(time);
            var output = _mapper.Map<TimesOfWorkDto>(result);
            _timesOfWorkService.CommitChanges();
            return Ok(output);

        }

        [Authorize(Roles = RoleNames.DoctorRole)]

        [HttpDelete("DeleteTimeOfDoctor")]
        public async Task<IActionResult> DeleteTimeOfDoctor(int TimeId)
        {
            var time = await _timesOfWorkService.FindByIdAsync(TimeId);
            if (time == null) return BadRequest("No time was found");
            var result = await _timesOfWorkService.Delete(time);
            var output = _mapper.Map<TimesOfWorkDto>(result);
            _timesOfWorkService.CommitChanges();
            return Ok(output);
        }
    }
}
