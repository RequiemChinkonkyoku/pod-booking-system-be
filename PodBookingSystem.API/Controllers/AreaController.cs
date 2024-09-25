using Microsoft.AspNetCore.Mvc;
using Models.DTOs;
using Services.Interface;

namespace PodBookingSystem.API.Controllers
{
    [ApiController]
    [Route("/Areas")]
    public class AreaController : Controller
    {
        private readonly IAreaService _areaService;
        public AreaController(IAreaService areaService)
        {
            _areaService = areaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllArea()
        {
            var areas = await _areaService.GetAllAreasAsync();
            return Ok(areas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAreaByID(int id)
        {
            try
            {
                var area = await _areaService.GetAreaByIDAsync(id);
                return Ok(area);
            }
            catch (Exception ex) when (ex.Message == "Area not Found")
            {
                return NotFound("Area not Found");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddArea([FromBody] AreaDto areaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var area = await _areaService.AddAreaAsync(areaDto);

            return Ok(area);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateArea(int id, [FromBody] AreaDto areaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updatedArea = await _areaService.UpdateAreaAsync(id, areaDto);
                if (updatedArea == null)
                {
                    return NotFound("Area not found.");
                }

                return Ok(updatedArea);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }
    }
}
