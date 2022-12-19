﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_clinic_api.DTOS;
using my_clinic_api.Interfaces;
using my_clinic_api.Models;
using my_clinic_api.Services;

namespace my_clinic_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patienService;
        private readonly IMapper _mapper;

        public PatientController(IPatientService patienService, IMapper mapper)
        {
            _patienService = patienService;
            _mapper = mapper;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var patients = await _patienService.GetAllAsync();
            if (patients == null)
                return NotFound();
            var result =  _mapper.Map<IEnumerable<PatientDto>>(patients);
            return Ok(result);
        }

        [HttpGet("GetAllWithData")]
        public async Task<IActionResult> GetAllWithData()
        {
            var patients = await _patienService.GetAllWithIncludeAsync(new List<string> { "RateAndReviews", "Bookings" });
            if (patients == null)
                return NotFound();
            var result = _mapper.Map<IEnumerable<PatientDto>>(patients);
            return Ok(result);
        }

        [HttpGet("GetPatientById")]
        public async Task<IActionResult> GetPatientById(string patientId)
        {
            var patient = await _patienService.FindPatientByIdAsync(patientId);

            if (patient == null) return NotFound($"No patient was found with ID {patientId}");

            var result = _mapper.Map<PatientDto>(patient);
            return Ok(result);
        }
        [HttpGet("GetPatientByIdWithData")]
        public async Task<IActionResult> GetPatientByIdWithData(string patientId)
        {
            var patient = await _patienService.FindPatientByIdWithIncludeAsync(patientId);

            if (patient == null) return NotFound($"No patient was found with ID {patientId}");

            var result = _mapper.Map<PatientDto>(patient);
            return Ok(result);
        }

        
    }
}
