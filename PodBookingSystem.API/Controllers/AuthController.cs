using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs;
using Services.Implement;
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

        [HttpPost("Verify/{otp}")]
        public async Task<IActionResult> VerifyOtp(string email, string otp)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(otp))
            {
                return BadRequest("Email and OTP code are required.");
            }

            try
            {
                // Use the service to verify the OTP code
                var isVerified = await _authService.VerifyOtpAsync(email, otp);

                if (isVerified)
                {
                    return Ok(new { message = "OTP verified successfully. User status has been updated." });
                }
                else
                {
                    return BadRequest(new { message = "Failed to verify OTP." });
                }
            }
            catch (ArgumentException ex)
            {
                // Return a bad request response with the error message
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                // Return a generic 500 error response for unexpected issues
                return StatusCode(500, new { message = "An unexpected error occurred. Please try again later.", error = ex.Message });
            }
        }
         
    }
}
