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

        [HttpGet("GetAllBookings")]
        public async Task<IActionResult> GetAllBookings()
        {
            var Bookings = await _bookService.GetAllAsync();

            if (Bookings == null) return NotFound();
            var result = _mapper.Map<IEnumerable<BookDto>>(Bookings);
            return Ok(result);
        }
        [HttpPost("AddBook")]
        public async Task<IActionResult> AddBook([FromForm] BookDto dto)
        {
            var book = new Book
            {
                DoctorId = dto.DoctorId,
                PatientId = dto.PatientId,
            };
            //var doctorWaiting = await _doctorService.GetWaitingTimeOfDoctor(dto.DoctorId);
            var checkTimeIsAvailable = await _timesOfWorkService.TimeIsAvailable(dto);
            if (!checkTimeIsAvailable) return NotFound("The doctor is not available at this time!");
            var checkBookIsAvailable = await _bookService.IsBookAvailable(dto);
            if (!checkBookIsAvailable) return NotFound("This time is already booked");
            var result = await _bookService.AddAsync(book);
            _bookService.CommitChanges();
            return Ok(result);

        }

    }
}
