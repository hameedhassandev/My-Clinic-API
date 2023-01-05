using Microsoft.AspNetCore.Mvc;
using my_clinic_api.Interfaces;
using my_clinic_api.Models;
using System.ComponentModel.DataAnnotations;

namespace my_clinic_api.Controllers
{

    [ApiController]
    public class ReasonsController : Controller
    {
        private readonly IReasonService _reasonService;

        public ReasonsController(IReasonService reasonService)
        {
            _reasonService = reasonService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var reasons = await _reasonService.GetAllAsync();
            if(reasons == null) return NotFound();
            return Ok(reasons);
        }

        [HttpGet("GetAllWithData")]
        public async Task<IActionResult> GetAllWithData()
        {
            var reasons = await _reasonService.GetAllWithData();
            if (reasons == null) return NotFound();
            return Ok(reasons);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int reasonId)
        {
            var reason = await _reasonService.FindByIdAsync(reasonId);
            if (reason == null) return NotFound();
            return Ok(reason);
        }

        [HttpGet("GetByIdWithData")]
        public async Task<IActionResult> GetByIdWithData(int reasonId)
        {
            var reason = await _reasonService.FindByIdWithData(reasonId);
            if (reason == null) return NotFound();
            return Ok(reason);
        
        }

        [HttpPost("AddReason")]
        public async Task<IActionResult> AddReason([FromForm , Required] string reasonName)
        {
            var reason = new ReportReasons
            {
                Reason = reasonName
            };
            var result = await _reasonService.AddAsync(reason);
            _reasonService.CommitChanges();
            if (result.Id == 0) return BadRequest();
            return Ok(result);
        }

        [HttpDelete("DeleteReason")]
        public async Task<IActionResult> DeleteReason([FromForm, Required] int reasonId)
        {
            var reason = await _reasonService.FindByIdAsync(reasonId);
            if (reason is null) return BadRequest("No reason in this id");
            var result = await _reasonService.Delete(reason);
            _reasonService.CommitChanges();
            return Ok(result.Id);
        }

    }
}
