using Microsoft.AspNetCore.Mvc;
using Models.DTOs;
using Services.Interface;

namespace PodBookingSystem.API.Controllers
{
    [ApiController]
    [Route("/PodType")]
    public class PodTypeController : ControllerBase
    {
        private readonly IPodTypeService _podTypeService;
        public PodTypeController(IPodTypeService podTypeService)
        {
            _podTypeService = podTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPodTypes() 
        { 
            var podTypes = await _podTypeService.GetAllPodTypesAsync();
            return Ok(podTypes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPodTypeByID(int id)
        {
            try
            {
                var podType = await _podTypeService.GetPodTypeByIDAsync(id);
                return Ok(podType);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddPodType([FromBody] PodTypeDto podTypeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var podType = await _podTypeService.AddPodTypeAsync(podTypeDto);
                return CreatedAtAction(nameof(GetPodTypeByID), new { id = podType.Id }, podType);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePodType(int id, [FromBody] PodTypeDto podTypeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var updatedPodType = await _podTypeService.UpdatePodTypeAsync(id, podTypeDto);
                return Ok(updatedPodType);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
