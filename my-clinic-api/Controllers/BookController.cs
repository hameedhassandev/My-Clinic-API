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

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet("GetAllBookings")]
        public async Task<IActionResult> GetAllBookings()
        {
            var result = await _bookService.GetAllAsync();

            if (result == null) return NotFound();

            return Ok(result);
        }
        [HttpPost("AddBook")]
        public async Task<IActionResult> AddBook([FromForm] BookDto dto)
        {
            var book = new Book
            {
                DoctorId = dto.DoctorId,
                PatientId = dto.PatientId,
                Time = dto.Time,
            };
            var result = await _bookService.AddAsync(book);
            _bookService.CommitChanges();
            return Ok(result);

        }

    }
}
