using Microsoft.AspNetCore.Mvc;
using my_clinic_api.Classes;
using my_clinic_api.Dto;
using my_clinic_api.Interfaces;
using my_clinic_api.Models;
using my_clinic_api.Services;
using System.Linq;
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
            if (result == null)
                return NotFound();

            return Ok(result);
        }


        // GET: api/Hospital/GetAll
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {

            var result = await _hospitalService.GetAllAsync();
            if (result == null)
                return NotFound();

            return Ok(result);
        }


        // GET: api/Hospital/GetAllPagination
        [HttpGet("GetAllPagination")]
        public async Task<IActionResult> GetAllPagination([FromQuery] int skip, [FromQuery] int take)
        {

            var result = await _hospitalService.GetAllPaginationAsync(skip, take);
            if (result == null)
                return NotFound();

            return Ok(result);
        }



        // GET: api/Hospital/GetAllWithFilteration
        [HttpGet("GetAllWithFilteration")]
        public async Task<IActionResult> GetAllWithFilteration([FromQuery] string searchKey)
        {

            Expression<Func<Hospital, bool>> predicate = h => h.Name.Contains(searchKey) || h.Address.Contains(searchKey);

            var result = await _hospitalService.FindAllAsync(predicate);
            if (result == null) return NotFound();

            return Ok(result);

        }

        // GET: api/Hospital/GetAllWithFilterationAndPagination
        [HttpGet("GetAllWithFilterationAndPagination")]
        public async Task<IActionResult> GetAllWithFilterationAndPagination([FromQuery] string searchKey, [FromQuery] int skip, [FromQuery] int take)
        {

            Expression<Func<Hospital, bool>> predicate = h => h.Name.Contains(searchKey) || h.Address.Contains(searchKey);

            var result = await _hospitalService.FindAllPaginationAsync(predicate, skip, take);
            if (result == null) return NotFound();

            return Ok(result);

        }

        // GET: api/Hospital/GetAllDoctorsInHospital

        //POST:api/Hospita/AddHospital
        [HttpPost("AddHospital")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddHospital([FromForm] HospitalDto dto)
        {
            var hospital = new Hospital
            {
                Name = dto.HospitalName,
                Address = dto.HospitalAddress
            };

            if (!ModelState.IsValid) return BadRequest();

            // if model is valid
            //check if name is exist
            Expression<Func<Hospital, bool>> predicate = h => h.Name.Equals(dto.HospitalName);

            var allHospital = await _hospitalService.FindAllAsync(predicate);

            var isExist = await _hospitalService.HospitalIsExist(dto.HospitalName);
            if (isExist) return BadRequest("Hospital name is exist");

            var result = await _hospitalService.AddAsync(hospital);
            _hospitalService.CommitChanges();

            return Ok(result);


        }


        /*     //PUT:api/Hospita/UpadteHospital
             [HttpPut("UpadteHospital")]
             public async Task<IActionResult> UpadteHospital([FromForm] HospitalDto dto)
             {

             }*/

    }
}
