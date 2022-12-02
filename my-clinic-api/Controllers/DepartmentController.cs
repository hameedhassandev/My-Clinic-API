using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

            var result = await _departmentService.GetAllWithSpecialistAsync();
            var getAllDepartmentWithSpecialist = result.Select(d => new { d.Id, d.Name, d.Description,d.specialists });


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



    }
}
