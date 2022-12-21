using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using my_clinic_api.DTOS;
using my_clinic_api.Interfaces;
using my_clinic_api.Models;
using System.Linq.Expressions;
using System.Numerics;
using System.IO;
namespace my_clinic_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {

        private readonly IDoctorService _doctorService;
        private readonly ISpecialistService _specialistService;
        private readonly IMapper _mapper;
        public UserManager<ApplicationUser> UserManager { get; }
        private readonly SignInManager<ApplicationUser> SignInManager;

        public DoctorController(IDoctorService doctorService,
            IMapper mapper, UserManager<ApplicationUser> _UserManager,
            SignInManager<ApplicationUser>
            _SignInManager, ISpecialistService specialistService)
        {
            _doctorService = doctorService;
            UserManager = _UserManager;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            SignInManager = _SignInManager;
            _specialistService = specialistService;
            
        }

        [HttpGet("GetAllDoctors")]
        public async Task<IActionResult> GetAllDoctors()
        {
            var doctors = await _doctorService.GetAllAsync();

            if (doctors == null) return NotFound();

           var result = _mapper.Map<IEnumerable<DoctorDto>>(doctors);
            return Ok(result);
        }
        [HttpGet("GetAllDoctorsWithData")]
        public async Task<IActionResult> GetAllDoctorsWithData()
        {
            var doctors = await _doctorService.GetAllWithData();

            if (doctors == null) return NotFound();

           var result = _mapper.Map<IEnumerable<DoctorDto>>(doctors);
            return Ok(result);
        }

        [HttpGet("GetDoctorById")]
        public async Task<IActionResult> GetDoctorById(string doctorId)
        {
            
            var doctor = await _doctorService.FindDoctorByIdAsync(doctorId);

            if (doctor == null) return NotFound($"No user was found with ID {doctorId}");

            var result = _mapper.Map<DoctorDto>(doctor);
            return Ok(result);
        }
        [HttpGet("GetDoctorByIdWithData")]
        public async Task<IActionResult> GetDoctorByIdWithData(string doctorId)
        {
            
            var doctor = await _doctorService.FindDoctorByIdWithDataAsync(doctorId);

            if (doctor == null) return NotFound($"No user was found with ID {doctorId}");

            var result = _mapper.Map<DoctorDto>(doctor);
            return Ok(result);
        }

        [HttpPost("AddSpecialistToDoctor")]
        public async Task<IActionResult> AddSpecialistToDoctor(string doctorId , int specialistId)
        {
            var result = await _specialistService.AddSpecialistToDoctor(doctorId, specialistId);
            if (result)
                return Ok("Specialist Added Successfully");
            return BadRequest();
        }

    }
}
