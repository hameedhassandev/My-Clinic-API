using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using my_clinic_api.DTOS;
using my_clinic_api.DTOS.CreateDto;
using my_clinic_api.Interfaces;
using my_clinic_api.Models;
using my_clinic_api.Services;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace my_clinic_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsuranceController : ControllerBase
    {
        private readonly IInsuranceService _insuranceService;
        private readonly IMapper _mapper;


        public InsuranceController(IInsuranceService insuranceService, IMapper mapper)
        {
            _insuranceService = insuranceService;
            _mapper = mapper;
        }
        // GET: api/Insurance/GetInsuranceById/{id}
        [HttpGet("GetInsuranceById/{id}")]
        public async Task<IActionResult> GetInsuranceById(int id)
        {

            var result = await _insuranceService.FindByIdAsync(id);

            if (result == null)
                return NotFound();
            var output = _mapper.Map<InsuranceDto>(result);    
            return Ok(result);
        }
        // GET: api/Insurance/GetInsuranceByIdWithDoctors/{id}
        [HttpGet("GetInsuranceWithDataById/{id}")]
        public async Task<IActionResult> GetInsuranceWithDoctorsById(int id )
        {
            var result = await _insuranceService.FindInsuranceByIdWithData(id);

            if (result == null)
                return NotFound();
            var output = _mapper.Map<InsuranceDto>(result);
            return Ok(output);
        }


        // GET: api/Insurance/GetAll
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {

            var result = await _insuranceService.GetAllAsync();
            if (result == null)
                return NotFound();
            var output = _mapper.Map<IEnumerable<InsuranceDto>>(result);
            return Ok(output);
        }

        // GET: api/Insurance/GetAllWithData
        [HttpGet("GetAllWithData")]
        public async Task<IActionResult> GetAllWithData()
        {
            var result = await _insuranceService.GetAllWithData();
            if (result == null)
                return NotFound();
            var output = _mapper.Map<IEnumerable<InsuranceDto>>(result);
            return Ok(output);
        }
        // GET: api/Insurance/GetAllPagination
        [HttpGet("GetAllPagination")]
        public async Task<IActionResult> GetAllPagination([FromQuery] int skip, [FromQuery] int take)
        {

            var result = await _insuranceService.GetAllPaginationAsync(skip, take);
            if (result == null)
                return NotFound();

            return Ok(result);
        }



        // GET: api/Insurance/GetAllWithFilteration
        [HttpGet("GetAllWithFilteration")]
        public async Task<IActionResult> GetAllWithFilteration([FromQuery] string searchKey)
        {

            Expression<Func<Insurance, bool>> predicate = h => h.CompanyName.Contains(searchKey);

            var result = await _insuranceService.FindAllAsync(predicate);
            if (result == null) return NotFound();

            return Ok(result);

        }

        // GET: api/Insurance/GetAllWithFilterationAndPagination
        [HttpGet("GetAllWithFilterationAndPagination")]
        public async Task<IActionResult> GetAllWithFilterationAndPagination([FromQuery] string searchKey, [FromQuery] int skip, [FromQuery] int take)
        {

            Expression<Func<Insurance, bool>> predicate = h => h.CompanyName.Contains(searchKey);

            var result = await _insuranceService.FindAllPaginationAsync(predicate, skip, take);
            if (result == null) return NotFound();

            return Ok(result);

        }
        // Post: api/Insurance/AddInsurance
        [HttpPost("AddInsurance")]
        public async Task<IActionResult> AddInsurance([FromForm] CreateInsuranceDto dto)
        {
            var insurance = new Insurance
            {
                CompanyName = dto.CompanyName,
                Discount = dto.Discount,
            };

            if (!ModelState.IsValid) return BadRequest();

            // if model is valid
            //check if name is exist
            var isExist = await _insuranceService.InsuranceNameIsExist(dto.CompanyName);
            if (isExist.Any()) return BadRequest("Insurance name is exist");
            var result = await _insuranceService.AddAsync(insurance);
            _insuranceService.CommitChanges();
            return Ok(result);
        }

        //PUT:api/Insurance/UpadteInsurance
        [HttpPut("UpadteInsurance")]
        public async Task<IActionResult> UpadteInsurance([FromForm, Required] int id, [FromForm] CreateInsuranceDto dto)
        {
            var insurance = await _insuranceService.FindByIdAsync(id);
            if (insurance == null)
                return NotFound($"No insurance was found with ID {id}");
            if (insurance.CompanyName == dto.CompanyName && insurance.Discount == dto.Discount)
                return BadRequest("No changes were found!");
            var checkName = await _insuranceService.InsuranceNameIsExist(dto.CompanyName);
            if (checkName.Any(h => h.Id != insurance.Id))
                return BadRequest("There is another insurance has this name!");
            insurance.CompanyName = dto.CompanyName;
            insurance.Discount = dto.Discount;
            var result = await _insuranceService.Update(insurance);

            _insuranceService.CommitChanges();
            return Ok(result);
        }

        //DELETE:api/Insurance/DeleteInsurance
        [HttpDelete("DeleteInsurance")]
        public async Task<IActionResult> DeleteInsurance([FromForm, Required] int id)
        {
            var insurance = await _insuranceService.FindByIdAsync(id);
            if (insurance == null)
                return NotFound($"No insurance was found with ID {id}");
            var result = await _insuranceService.Delete(insurance);
            _insuranceService.CommitChanges();
            return Ok(result);
        }

    }
}
