using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_clinic_api.Dto.AuthDtos;
using my_clinic_api.Interfaces;

namespace my_clinic_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("RegisterAsPatient")]
        public async Task<IActionResult> RegisterAsPatient([FromBody] UserRegisterDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.UserRegisterAsync(model);

            if (!result.IsAuth)
                return BadRequest(result.Massage);

            //SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);

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

            //SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);

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

            //SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);

            return Ok(result);
        }

    }
}
