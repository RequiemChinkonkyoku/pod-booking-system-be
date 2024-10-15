using Microsoft.AspNetCore.Mvc;
using Services.Interface;

namespace PodBookingSystem.API.Controllers
{
    [ApiController]
    [Route("/Schedules")]
    public class ScheduleController : Controller
    {
        private readonly IScheduleService _scheduleService;
        public ScheduleController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _scheduleService.GetAllSchedulesAsync();
            return Ok(response);
        }
    }
}
