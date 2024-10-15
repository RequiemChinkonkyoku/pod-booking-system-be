using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;

namespace PodBookingSystem.API.Controllers
{
    [ApiController]
    [Route("/Slot")]
    public class SlotController : ControllerBase
    {
        private readonly ISlotService _slotService;
        public SlotController(ISlotService slotService)
        {
            _slotService = slotService;
        }

        [HttpGet("PodType/{id}")]
        public async Task<IActionResult> GetSlotByPodType(int id)
        {
            var slots = await _slotService.GetSlotBySlotTypeAsync(id);
            if (slots == null || !slots.Any())
            {
                return NotFound("No slots found for this pod type.");
            }
            return Ok(slots);
        }
    }
}
