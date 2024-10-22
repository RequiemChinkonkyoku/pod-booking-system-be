using Microsoft.AspNetCore.Mvc;
using Models.DTOs;
using Services.Implement;
using Services.Interface;

namespace PodBookingSystem.API.Controllers
{
    [ApiController]
    [Route("/Pods")]
    public class PodController : ControllerBase
    {
        private readonly IPodService _podService;

        public PodController(IPodService podService)
        {
            _podService = podService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPods()
        {
            var pods = await _podService.GetAllPodsAsync();
            return Ok(pods);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPodByID(int id)
        {
            try
            {
                var pod = await _podService.GetPodByIDAsync(id);
                return Ok(pod);
            }
            catch (Exception ex) when (ex.Message == "Pod not Found")
            {
                return NotFound("Pod not Found");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddPod([FromBody] PodDto podDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var pod = await _podService.AddPodAsync(podDto);
                return CreatedAtAction(nameof(GetPodByID), new { id = pod.Id }, pod);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePod(int id, [FromBody] PodDto podDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updatedPod = await _podService.UpdatePodAsync(id, podDto);
                return Ok(updatedPod);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePod(int id)
        {
            try
            {
                var deletedPod = await _podService.DeletePodAsync(id);
                return Ok($"Pod {id} deleted successfully.");
            }
            catch (Exception ex)
            {
                if (ex.Message == "Pod not found")
                {
                    return NotFound("Pod not found");
                }
                return StatusCode(500, "An error occurred while deleting the pod.");
            }
        }
        [HttpGet("Available/{podTypeId}")]
        public async Task<IActionResult> GetAvailablePods(int podTypeId, int scheduleId, DateOnly arrivalDate)
        {
            if (podTypeId <= 0 || scheduleId <= 0 )
            {
                return BadRequest("Invalid input parameters.");
            }
            if (arrivalDate < DateOnly.FromDateTime(DateTime.Now))
            {
                return BadRequest("Arrival Date must be today or further");
            }
            var availablePods = await _podService.GetAvailablePodsByPodTypeAsync(podTypeId, scheduleId, arrivalDate);

            if (availablePods == null || !availablePods.Any())
            {
                return NotFound("No available pods found for the given parameters.");
            }

            return Ok(availablePods);
        }
    }
}
