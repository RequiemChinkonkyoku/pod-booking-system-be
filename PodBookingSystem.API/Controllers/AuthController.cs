using Microsoft.AspNetCore.Mvc;
using Models.DTOs;
using Services.Interface;

namespace PodBookingSystem.API.Controllers
{
    [ApiController]
    [Route("/Auth")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            var response = await _authService.Login(email, password);
            return Ok(new { Token = response });
        }

        [HttpPost("Register/Customer")]
        public async Task<IActionResult> RegisterCustomer([FromBody] CreateUserDto request)
        {
            try
            {
                var response = await _authService.RegisterCustomer(request);
                return Ok(response);
                //return CreatedAtAction(nameof(UserController.GetUserById), new { id = response.Id }, response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred. " + ex.Message);
            }
        }
    }
}
