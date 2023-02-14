using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using my_clinic_api.Classes;
using my_clinic_api.Dto.AuthDtos;
using my_clinic_api.DTOS;
using my_clinic_api.DTOS.AuthDtos;
using my_clinic_api.Interfaces;
using my_clinic_api.Models;
using System.Data;

namespace my_clinic_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context; 


        public AuthController(IAuthService authService, IMapper mapper, ApplicationDbContext contxt)
        {
            _authService = authService;
            _mapper = mapper;
            _context = contxt;
        }

        [HttpPost("RegisterAsPatient")]
        public async Task<IActionResult> RegisterAsPatient([FromBody] UserRegisterDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.UserRegisterAsync(model);

            if (!result.IsAuth)
                return BadRequest(result.Massage);

            return Ok(result);
        }

        [HttpPost("RegisterAsDoctor")]
        public async Task<IActionResult> RegisterAsDoctor([FromBody] DoctorRegisterDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.DoctorRegisterAsync(model,false);

            if (!result.IsAuth)
                return BadRequest(result.Massage);
            
            //setTokenInCookie(result.Token, (DateTime)result.ExpiresOn);

            return Ok(result);
        }

        [HttpPost("DoctorRegister")]
        public async Task<IActionResult> DoctorRegister([FromForm] DoctorRegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.testRegisteration(dto);

            if (!result.IsAuth)
                return BadRequest(result.Massage);


            return Ok(result);
        }

        [HttpPut("updateProfilePic")]
        public async Task<IActionResult> updateProfilePic([FromForm] updateImageDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.updateProfilePic(dto);

            if (!result.IsAuth)
                return BadRequest(result.Massage);


            return Ok(result);
        }

        [HttpPut("updateDoctor")]
        public async Task<IActionResult> updateDoctor([FromForm] updateDoctorDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.updatDoctor(dto);

            if (!result.IsAuth)
                return BadRequest(result.Massage);


            return Ok(result);
        }



        [HttpPost("AddDoctorByAdmin")]
        public async Task<IActionResult> AddDoctorByAdmin([FromBody] DoctorRegisterDto model, bool isConfirmed)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.DoctorRegisterAsync(model, isConfirmed);

            if (!result.IsAuth)
                return BadRequest(result.Massage);
//
           setTokenInCookie(result.Token, (DateTime)result.ExpiresOn);

            return Ok(result);
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] TokenRequestModelDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
                
            var result = await _authService.GetTokenAsync(model);

            if (!result.IsAuth)
                return BadRequest(result.Massage);


                if (!string.IsNullOrEmpty(result.Token))
                setTokenInCookie(result.Token, (DateTime)result.ExpiresOn);


            return Ok(result);
        }


        [HttpPost("AddNewRole")]
        public async Task<IActionResult> AddNewRole(RoleNameDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var isRoleExist = await _authService.AddRole(dto.Name);
            if(!isRoleExist)
                return BadRequest($"Role {dto.Name} is exist");  

            return Ok($"Role {dto.Name} added successfully");
        }

        [HttpPost("ConfirmDoctorWithEmail")]
        public async Task<IActionResult> ConfirmDoctorWithEmail()
        {
            var result = await _authService.confirmationMailDoctor("0576eab3-6c8e-4d69-a574-2a0813f6fbd3");
            if (!result) return BadRequest("somthing err");


            return Ok("Done");
        }


        [HttpPost("ConfirmUserMail")]
        public async Task<IActionResult> ConfirmUserMail(confirmMailDto dto)
        {
            var result = await _authService.ConfirmUserEmail(dto);
            if (!result) return BadRequest();

            return Ok("user confirmed");
        }

        [HttpGet("AllRoles")]
        public async Task<IActionResult> AllRoles()
        {

           var roles =  await _authService.GetRoles();
            if (roles == null) return NotFound();

            var result = _mapper.Map<List<RolesDto>>(roles);
            return Ok(result);
        }


        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail()
        {

            return Ok();
        }


        [HttpPost("")]


        //save token in cookie
        private void setTokenInCookie(string token, DateTime expiresOn)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = expiresOn.ToLocalTime(),
            };
            Response.Cookies.Append("Token", token, cookieOptions);
        }

        [HttpGet("TestList")]
        public async Task<IActionResult> testLList()
        {
            var ddl = new DoctorDropDownDto();
            var lis = new List<SelectList>();
            var hospitals = await _context.Hospitals.ToListAsync();
            SelectList objj = new SelectList(hospitals, "Id", "Name");

            lis.Add(objj);
            return Ok(lis);

        }

    }
}
