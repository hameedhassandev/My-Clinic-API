﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_clinic_api.Classes;
using my_clinic_api.DTOS;
using my_clinic_api.DTOS.CreateDto;
using my_clinic_api.Interfaces;
using my_clinic_api.Models;
using my_clinic_api.Services;
using System.Data;

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

        [Authorize(Roles = RoleNames.AdminRole)]

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var Bookings = await _bookService.GetAllAsync();

            if (Bookings == null) return NotFound();
            var result = _mapper.Map<IEnumerable<BookDto>>(Bookings);
            return Ok(result);
        }

        [Authorize(Roles = RoleNames.AdminRole)]

        [HttpGet("GetAllWithData")]
        public async Task<IActionResult> GetAllWithData()
        {
            var Bookings = await _bookService.GetAllWithData();

            if (Bookings == null) return NotFound();
            var result = _mapper.Map<IEnumerable<BookDto>>(Bookings);
            return Ok(result);
        }

        [Authorize(Roles = RoleNames.AdminRole)]

        [Authorize(Roles = RoleNames.DoctorRole)]

        [HttpGet("GetBookingsOfDoctor")]
        public async Task<IActionResult> GetBookingsOfDoctor(string doctorId)
        {
            var Bookings = await _bookService.GetBookingsOfDoctor(doctorId);
            if (Bookings == null) return NotFound();
            var result = _mapper.Map<IEnumerable<BookDto>>(Bookings);
            return Ok(result);
        }

        [Authorize(Roles = RoleNames.AdminRole)]

        [Authorize(Roles = RoleNames.DoctorRole)]
        [HttpGet("GetBookingsOfPatient")]
        public async Task<IActionResult> GetBookingsOfPatient(string patientId)
        {
            var Bookings = await _bookService.GetBookingsOfPatient(patientId);
            if (Bookings == null) return NotFound();
            var result = _mapper.Map<IEnumerable<BookDto>>(Bookings);
            return Ok(result);
        }


        [Authorize(Roles = RoleNames.DoctorRole)]
        [HttpPut("ConfirmBook")]
        public async Task<IActionResult> ConfirmBook([FromForm]int bookId)
        {
            var confirm = await _bookService.ConfirmBook(bookId);
            if (!confirm) return BadRequest("somthing err");

            return Ok();
        }


        [Authorize(Roles = RoleNames.PatientRole)]
        [HttpPost("AddBook")]
        public async Task<IActionResult> AddBook([FromForm] CreateBookDto dto)
        {
            var add = await _bookService.AddBook(dto);
            if (add != "Success") return BadRequest(add);
            /// Return issue to display the object 
            return Ok(dto);
        }



    }
}
