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
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly ITimesOfWorkService _timesOfWorkService;
        private readonly IDoctorService _doctorService;
        private readonly IMapper _mapper;
        

        public BookController(IBookService bookService, ITimesOfWorkService timesOfWorkService, IDoctorService doctorService, IMapper mapper)
        {
            _bookService = bookService;
            _timesOfWorkService = timesOfWorkService;
            _doctorService = doctorService;
            _mapper = mapper;
        }
        

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var Bookings = await _bookService.GetAllAsync();

            if (Bookings == null) return NotFound();
            var result = _mapper.Map<IEnumerable<BookDto>>(Bookings);
            return Ok(result);
        }
        [HttpGet("GetAllWithData")]
        public async Task<IActionResult> GetAllWithData()
        {
            var Bookings = await _bookService.GetAllWithData();

            if (Bookings == null) return NotFound();
            var result = _mapper.Map<IEnumerable<BookDto>>(Bookings);
            return Ok(result);
        }
        [HttpPost("AddBook")]
        public async Task<IActionResult> AddBook([FromForm] BookDto dto)
        {
            //check that the time not passed
            if (dto.Time < DateTime.Now) return NotFound("This time has passed!");
            //Check if doctor has no working days
            var allTimes = await _timesOfWorkService.GetTimesOfDoctor(dto.DoctorId);
            if(allTimes == null || !allTimes.Any()) return NotFound("This doctor is not available!");
            // Get working Times Of specific DayOfWeak that user chooses 
            var timeOfDay = allTimes.SingleOrDefault(d => d.day.ToString() == dto.Time.DayOfWeek.ToString());
            //Ensure that the time is in range of doctor's working time
            var checkTimeIsAvailable = await _timesOfWorkService.TimeIsAvailable(dto);
            if (!checkTimeIsAvailable || timeOfDay is null) return NotFound("The doctor is not available at this time!");
            //Ensure that no bookings at same time 
            var checkBookIsAvailable = await _bookService.IsBookAvailable(dto); 
            if (!checkBookIsAvailable) return NotFound("This time is already booked!");
            
            var book = new Book
            {
                DoctorId = dto.DoctorId,
                PatientId = dto.PatientId,
                Time = dto.Time,
                CreatedAt = DateTime.Now,
                ExpiryDate = new DateTime(dto.Time.Year,dto.Time.Month,
                    dto.Time.Day, timeOfDay.EndWork.TimeOfDay.Hours,
                    timeOfDay.EndWork.TimeOfDay.Minutes,
                    timeOfDay.EndWork.TimeOfDay.Seconds),
            };
            var result = await _bookService.AddAsync(book);
            var output = _mapper.Map<BookDto>(result);
            _bookService.CommitChanges();
            return Ok(output);

        }

    }
}
