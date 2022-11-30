using Microsoft.AspNetCore.Mvc;
using my_clinic_api.Classes;
using my_clinic_api.Interfaces;
using my_clinic_api.Models;
using System.Linq.Expressions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace my_clinic_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalController : ControllerBase
    {
        private readonly IHospitalService _hospitalService;
        public HospitalController(IHospitalService hospitalService)
        {
            _hospitalService = hospitalService;
        }

        // GET: api/Hospital/GetById/5
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        { 

            var result = await _hospitalService.GetByIdAsync(id);
            if(result == null)
                return NotFound();

            return Ok(result);
        }


        // GET: api/Hospital/GetAll
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] Filters? filters)
        {
        
            var result = await _hospitalService.GetAllAsync();
            if (result == null)
                return NotFound();

                return Ok(result);
            }
            
        }

      

}
