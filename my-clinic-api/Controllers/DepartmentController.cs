﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_clinic_api.DTOS;
using my_clinic_api.DTOS.CreateDto;
using my_clinic_api.Interfaces;
using my_clinic_api.Models;
using my_clinic_api.Services;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace my_clinic_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;
        public DepartmentController(IDepartmentService departmentService, IMapper mapper)
        {
            _departmentService = departmentService;
            _mapper = mapper;
        }

        [HttpGet("GetDepartmentById/{id}")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {

            var result = await _departmentService.FindByIdAsync(id);
            if (result == null)
                return NotFound();
            var output = _mapper.Map<DepartmentDto>(result);
            return Ok(output);
        }
        
        [HttpGet("GetDepartmentByIdWithData/{id}")]
        public async Task<IActionResult> GetDepartmentByIdWithData(int id)
        {

            var result = await _departmentService.FindDepartmentByIdWithData(id);
            if (result == null)
                return NotFound();
            var output = _mapper.Map<DepartmentDto>(result);
            return Ok(output);
        }



        // GET: api/Department/GetAll
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {

            var result = await _departmentService.GetAllAsync();


            if (result == null)
                return NotFound();
            var output = _mapper.Map<IEnumerable<DepartmentDto>>(result);
            return Ok(output);
        }

        // GET: api/Department/GetAllWithData
        [HttpGet("GetAllWithData")]
        public async Task<IActionResult> GetAllWithData()
        {
            var result = await _departmentService.GetAllWithData();
            if (result == null)
                return NotFound();
            var output = _mapper.Map<IEnumerable<DepartmentDto>>(result);
            return Ok(output);
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
        public async Task<IActionResult> AddDepratment([FromForm] CreateDepartmentDto dto)
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
        public async Task<IActionResult> UpadteDepartment(int id, [FromForm] CreateDepartmentDto dto)
        {
            var department = await _departmentService.FindByIdAsync(id);
            if (department == null)
                return NotFound($"No department was found with ID {id}");
            if (department.Name == dto.Name && department.Description == dto.Description)
                return BadRequest("No changes were found!");
            var checkName = await _departmentService.DepartmentNameIsExist(dto.Name);
            if (checkName.Any(h => h.Id != department.Id))
                return BadRequest("There is another hospital has this name!");
            department.Name = dto.Name;
            department.Description = dto.Description;
            var result = await _departmentService.Update(department);

            _departmentService.CommitChanges();
            return Ok(result);
        }

        //DELETE:api/Department/DeleteDepartment
        [HttpDelete("DeleteDepartment")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var department = await _departmentService.FindByIdAsync(id);

            if (department == null)
                return NotFound($"No department was found with ID {id}");

            var result = await _departmentService.Delete(department);

            _departmentService.CommitChanges();
            return Ok(result);
        }

    }
}
