using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_clinic_api.Dto;
using my_clinic_api.Interfaces;
using my_clinic_api.Models;
using my_clinic_api.Services;
using System;
using System.Linq.Expressions;

namespace my_clinic_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet("GetDepartmentById/{id}")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {

            var result = await _departmentService.GetByIdAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }



        // GET: api/Department/GetAll
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {

            var result = await _departmentService.GetAllAsync();
            var getNameAndDesc = result.Select(d => new { d.Id, d.Name, d.Description });


            if (getNameAndDesc == null)
                return NotFound();

            return Ok(getNameAndDesc);
        }

        // GET: api/Department/GetAllWithSpicialist
        [HttpGet("GetAllWithSpecialist")]
        public async Task<IActionResult> GetAllWithSpecialist()
        {
            var result = await _departmentService.GetAllWithIncludeAsync(new List<string>() { "specialists" });
            var getAllDepartmentWithSpecialist = result.Select(d => new { d.Id, d.Name, d.Description, Specialists = d.specialists.Select(d=> new {d.Id , d.SpecialistName }) });
            if (getAllDepartmentWithSpecialist == null)
                return NotFound();
            return Ok(getAllDepartmentWithSpecialist);
        }
        [HttpGet("GetAllWithDoctor")]
        public async Task<IActionResult> GetAllWithDoctor()
        {
            var result = await _departmentService.GetAllWithIncludeAsync(new List<string>() { "doctors" });
            var GetAllWithDoctor = result.Select(d => new { d.Id, d.Name, d.Description, d.doctors });
            if (GetAllWithDoctor == null)
                return NotFound();
            return Ok(GetAllWithDoctor);
        }
        // GET: api/Department/GetAllWithSpicialistAndDoctors
        [HttpGet("GetAllWithSpicialistAndDoctors")]
        public async Task<IActionResult> GetAllWithSpicialistAndDoctors()
        {
            var result = await _departmentService.GetAllWithIncludeAsync(new List<string>() { "specialists" , "doctors" });
            var getAllDepartmentWithSpecialist = result.Select(d => new { d.Id, d.Name, d.Description, Specialists = d.specialists.Select(d => new { d.Id, d.SpecialistName }) , d.doctors });
            if (getAllDepartmentWithSpecialist == null)
                return NotFound();
            return Ok(getAllDepartmentWithSpecialist);
        }


        // GET: api/Department/GetAllPagination
        [HttpGet("GetAllPagination")]
        public async Task<IActionResult> GetAllPagination([FromQuery] int skip, [FromQuery] int take)
        {

            var result = await _departmentService.GetAllPaginationAsync(skip, take);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // GET: api/Department/GetAllWithFilteration
        [HttpGet("GetAllWithFilteration")]
        public async Task<IActionResult> GetAllWithFilteration([FromQuery] string searchKey)
        {
            Expression<Func<Department, bool>> predicate =
                h => h.Name.Contains(searchKey) || h.Description.Contains(searchKey);
            var result = await _departmentService.FindAllAsync(predicate);
            if (result == null) return NotFound();

            return Ok(result);

        }

        // GET: api/Department/GetAllWithFilterationAndPagination
        [HttpGet("GetAllWithFilterationAndPagination")]
        public async Task<IActionResult> GetAllWithFilterationAndPagination([FromQuery] string searchKey, [FromQuery] int skip, [FromQuery] int take)
        {

            Expression<Func<Department, bool>> predicate =
                h => h.Name.Contains(searchKey) || h.Description.Contains(searchKey);
            var result = await _departmentService.FindAllPaginationAsync(predicate, skip, take);
            if (result == null) return NotFound();

            return Ok(result);
        }
        // Post: api/Department/AddDepartment
        [HttpPost("AddDepartment")]
        public async Task<IActionResult> AddDepratment([FromForm] DepartmentDto dto)
        {
            var department = new Department
            {
                 Name = dto.Name,
                Description = dto.Description
            };

            if (!ModelState.IsValid) return BadRequest();

            // if model is valid
            //check if name is exist
            var isExist = await _departmentService.DepartmentNameIsExist(dto.Name);
            if (isExist.Any()) return BadRequest("Department name is exist");
            var result = await _departmentService.AddAsync(department);
            _departmentService.CommitChanges();
            return Ok(result);
        }

        //PUT:api/Department/UpadteDepartment
        [HttpPut("UpadteDepartment")]
        public async Task<IActionResult> UpadteDepartment(int id, [FromForm] DepartmentDto dto)
        {
            var department = await _departmentService.GetByIdAsync(id);
            if (department == null)
                return NotFound($"No department was found with ID {id}");
            if (department.Name == dto.Name && department.Description == dto.Description)
                return BadRequest("No changes were found!");
            var checkName = await _departmentService.DepartmentNameIsExist(dto.Name);
            if (checkName.Any(h => h.Id != department.Id))
                return BadRequest("There is another hospital has this name!");
            department.Name = dto.Name;
            department.Description = dto.Description;
            var result = _departmentService.Update(department);

            _departmentService.CommitChanges();
            return Ok(result);
        }

        //DELETE:api/Department/DeleteDepartment
        [HttpDelete("DeleteDepartment")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var department = await _departmentService.GetByIdAsync(id);

            if (department == null)
                return NotFound($"No department was found with ID {id}");

            var result = _departmentService.Delete(department);

            _departmentService.CommitChanges();
            return Ok(result);
        }

    }
}
