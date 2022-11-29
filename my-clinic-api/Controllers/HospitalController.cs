using Microsoft.AspNetCore.Mvc;
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


        // GET: api/Hospital/GetById/5
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            Expression<Func<Hospital, bool>> predicate = h => h.Id < 4;
            var result = await _hospitalService.FindAllAsync(predicate);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // GET: api/Hospital/GetById/5
        [HttpGet("GetAllPagination")]
        public async Task<IActionResult> GetAllPagination()
        {
            Expression<Func<Hospital, bool>> predicate = h => h.Id < 5;
            var result = await _hospitalService.FindAllAsync(predicate,2,2);
            if (result == null)
                return NotFound();

            return Ok(result);
        }


        // POST api/<HospitalController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<HospitalController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<HospitalController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
