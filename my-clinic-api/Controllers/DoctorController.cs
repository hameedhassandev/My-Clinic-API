using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using my_clinic_api.Dto;
using my_clinic_api.Interfaces;
using my_clinic_api.Models;
using System.Linq.Expressions;
using System.Numerics;

namespace my_clinic_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {

        private readonly IDoctorService _doctorService;
        private readonly IMapper _mapper;
        public UserManager<ApplicationUser> UserManager { get; }
        private readonly SignInManager<ApplicationUser> SignInManager;

        public DoctorController(IDoctorService doctorService,
            IMapper mapper, UserManager<ApplicationUser> _UserManager,
            SignInManager<ApplicationUser>
            _SignInManager)
        {
            _doctorService = doctorService;
            UserManager = _UserManager;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            SignInManager = _SignInManager;

        }

        [HttpGet("GetDoctorById")]
        public async Task<IActionResult> GetDoctorById(string userId)
        {
            var doctor = await _doctorService.FindDoctorByIdAsync(userId);

            if (doctor == null) return NotFound();

            var result = _mapper.Map<DoctorDto>(doctor);
            return Ok(result);
        }

        [HttpGet("GetDoctorByIdWithDepartment")]
        public async Task<IActionResult> GetDoctorByIdWithDepartment(string userId)
        {
            Expression<Func<Doctor, bool>> predicate = h => h.Id == userId;
            var doctor = await _doctorService.FindWithIncludesAsync(predicate, new List<string> {  "Hospitals" });

            if (doctor == null) return NotFound();

            var result = _mapper.Map<DoctorDto>(doctor);
            return Ok(result);
        }
        [HttpGet("GetDoctorByIdTestInclude")]
        public async Task<IActionResult> GetDoctorByIdTestInclude(string userId)
        {
            var doctor = await _doctorService.FindDoctorByIdSync(userId);

            if (doctor == null) return NotFound($"No user was found with ID {userId}");

            var result = _mapper.Map<DoctorDto>(doctor);
            return Ok(result);
        }

    }
}
