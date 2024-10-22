using Microsoft.AspNetCore.Mvc;
using Services.Interface;

namespace PodBookingSystem.API.Controllers
{
    [Route("[controller]")]
    public class MethodController : Controller
    {
        private readonly IMethodService _methodService;

        public MethodController(IMethodService methodService)
        {
            _methodService = methodService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMethod()
        {
            var response = await _methodService.GetAllMethod();

            return Ok(response);
        }

    }
}
