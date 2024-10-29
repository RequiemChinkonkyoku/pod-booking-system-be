using Microsoft.AspNetCore.Mvc;
using Models.DTOs;
using Services.Implement;
using Services.Interface;

namespace PodBookingSystem.API.Controllers
{
    [ApiController]
    [Route("/StaffArea")]
    public class StaffAreaController : Controller
    {
        private readonly IStaffAreaService _staffAreaService;
        public StaffAreaController(IStaffAreaService staffAreaService)
        {
            _staffAreaService = staffAreaService;
        }

        [HttpPost]
        public async Task<IActionResult> AssignStaffArea([FromBody] AssignStaffAreaDto assignStaffAreaDto)
        {
            try
            {
                var staffArea = await _staffAreaService.AssignStaffAreaAsync(assignStaffAreaDto);
                return Ok(staffArea);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStaffArea(int id, [FromBody] AssignStaffAreaDto AssignStaffAreaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updatedStaffArea = await _staffAreaService.UpdateStaffAreaAsync(id, AssignStaffAreaDto);
                if (updatedStaffArea == null)
                {
                    return NotFound("StaffArea not found.");
                }

                return Ok(updatedStaffArea);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }
    }
}
