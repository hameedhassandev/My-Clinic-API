﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_clinic_api.Classes;
using my_clinic_api.DTOS;
using my_clinic_api.DTOS.UpdateDro;
using my_clinic_api.Interfaces;
using my_clinic_api.Models;
using my_clinic_api.Services;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace my_clinic_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreaController : ControllerBase
    {
        private readonly IAreaService _areaService;

        public AreaController(IAreaService areaService)
        {
            _areaService = areaService;
        }



        // GET: api/Area/GetAreaById/{id}
        [HttpGet("GetAreaById/{id}")]
        public async Task<IActionResult> GetAreaById(int id)
        {

            var result = await _areaService.FindByIdAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // GET: api/Area/GetAllAreas/
        [HttpGet("GetAllAreas")]
        public async Task<IActionResult> GetAllAreas()
        {
            var result = await _areaService.GetAllAsync();
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // GET: api/Area/GetAllAreasByDepartmentId/city/1
        [HttpGet("GetAllAreasByCityId/{cityId}")]
        public async Task<IActionResult> GetAllAreasByCityId(int cityId)
        {
            var result = await _areaService.getAreaByCityId(cityId);
            if (result == null)
                return NotFound();

            return Ok(result);
        }


        // POST: api/Area/AddArea/
        [Authorize(Roles = RoleNames.AdminRole)]

        [HttpPost("AddArea")]
        public async Task<IActionResult> AddArea([FromForm] AreaDto dto)
        {
            var area = new Area
            {
                City = dto.City,
                AreaName = dto.AreaName
            };

            if (!ModelState.IsValid) return BadRequest();

            // if model is valid
            //check if name is exist
            var isExist = await _areaService.AreaNameIsExist(dto.AreaName);
            if (isExist.Any()) return BadRequest("Area name is exist");
            var result = await _areaService.AddAsync(area);
            _areaService.CommitChanges();

            return Ok(result);
        }


        //PUT:api/Area/UpadteArea/id
        [Authorize(Roles = RoleNames.AdminRole)]

        [HttpPut("UpadteArea")]
        public async Task<IActionResult> UpadteArea([FromForm] updateAreaDto dto)
        {
            var area = await _areaService.FindByIdAsync(dto.Id);
            if (area == null)
                return NotFound($"No area was found with ID {dto.Id}");
            if (area.AreaName == dto.AreaName && area.City == dto.City)
                return BadRequest("No changes were found!");
            var checkareaName = await _areaService.AreaNameIsExist(dto.AreaName);
            if (checkareaName.Any(h => h.Id != area.Id))
                return BadRequest("There is another area has this name!");
            area.AreaName = dto.AreaName;
            area.City = dto.City;
            var result = await _areaService.Update(area);

            _areaService.CommitChanges();
            return Ok(result);
        }

        [Authorize(Roles = RoleNames.AdminRole)]

        [HttpDelete("DeleteArea")]
        public async Task<IActionResult> DeleteArea([FromForm, Required] int id)
        {
            var area = await _areaService.FindByIdAsync(id);

            if (area == null)
                return NotFound($"No area was found with ID {id}");

            var result = await _areaService.Delete(area);

            _areaService.CommitChanges();
            return Ok(result);
        }

    }
}
