using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.IdentityModel.Tokens;
using Models.DTOs;
using Services.Interface;
using System.Diagnostics;
using System.Security.Claims;

namespace PodBookingSystem.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;

        }

        [HttpGet]
        public async Task<IActionResult> GetAllBookings()
        {
            int userId = 0;

            try
            {
                userId = Int32.Parse(User.FindFirst(ClaimTypes.Name).Value.ToString());
            }
            catch (Exception ex)
            {
                return Unauthorized("You must login to perform this task.");
            }

            var response = await _bookingService.GetAllBookings();

            return Ok(response);
        }

        [HttpGet("customer")]
        public async Task<IActionResult> GetCustomerBooking()
        {
            int userId = 0;

            try
            {
                userId = Int32.Parse(User.FindFirst(ClaimTypes.Name).Value.ToString());
            }
            catch (Exception ex)
            {
                return Unauthorized("You must login to perform this task.");
            }

            var response = await _bookingService.GetUserBookings(userId);

            return Ok(response);
        }

        [HttpGet("customer/{id}")]
        public async Task<IActionResult> ViewUserBookingById([FromRoute] int id)
        {
            int userId = 0;

            try
            {
                userId = Int32.Parse(User.FindFirst(ClaimTypes.Name)?.Value.ToString());
            }
            catch (Exception)
            {
                return Unauthorized("You must login to perform this task.");
            }

            var response = await _bookingService.GetBookingById(id, userId);

            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }

        [HttpPost("create-booking")]
        public async Task<IActionResult> CreateBooking([FromBody] CreateBookingRequest request)
        {
            int userId = 0;

            try
            {
                userId = Int32.Parse(User.FindFirst(ClaimTypes.Name).Value.ToString());
            }
            catch (Exception ex)
            {
                return Unauthorized("You must login to perform this task.");
            }

            var response = await _bookingService.CreateBooking(request, userId);

            if (response.Success)
            {
                return Ok(response.Booking);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }

        [HttpPut("cancel-booking/{id}")]
        public async Task<IActionResult> CancelBooking([FromRoute] int id)
        {
            int userId = 0;

            try
            {
                userId = Int32.Parse(User.FindFirst(ClaimTypes.Name)?.Value.ToString());
            }
            catch (Exception ex)
            {
                return Unauthorized("You must login to perform this task.");
            }

            var response = await _bookingService.CancelBooking(id, userId);

            if (response.Success)
            {
                return Ok(response.Booking);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }

        [HttpPut("update-booking/{id}")]
        public async Task<IActionResult> UpdateBooking([FromRoute] int id, [FromBody] UpdateBookingRequest request)
        {
            int userId = 0;

            try
            {
                userId = Int32.Parse(User.FindFirst(ClaimTypes.Name)?.Value.ToString());
            }
            catch (Exception ex)
            {
                return Unauthorized("You must login to perform this task.");
            }

            var response = await _bookingService.UpdateBooking(id, request, userId);

            if (response.Success)
            {
                return Ok(response.Booking);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }
    }
}
