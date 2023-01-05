using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;
using my_clinic_api.Interfaces;
using my_clinic_api.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;

namespace my_clinic_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnumsController : ControllerBase
    {
        private readonly IEnumBaseRepositry _enum;
        
        public EnumsController(IEnumBaseRepositry enums)
        {
            _enum = enums;
        }



        [HttpGet("GetValuesOfCities")]
        public async Task<IActionResult> GetValuesOfCities()
        {
            var data = _enum.GetValuesOfEnums(typeof(Cities));
            return Ok(data);
        }

        [HttpGet("GetValuesOfGender")]
        public async Task<IActionResult> GetValuesOfGender()
        {
            var data = _enum.GetValuesOfEnums(typeof(Gender));
            return Ok(data);
        }

        [HttpGet("GetValuesOfDays")]
        public async Task<IActionResult> GetValuesOfDays()
        {
            var data = _enum.GetValuesOfEnums(typeof(Days));
            return Ok(data);
        }

    }
}
