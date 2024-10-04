using Microsoft.AspNetCore.Mvc;
using Models.DTOs;
using Services.Interface;

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

        [HttpGet("view-booking/{id}")]
        public async Task<IActionResult> ViewBooking([FromRoute] int id)
        {
            var response = await _bookingService.GetUserBookings(id);

            return Ok(response);
        }

        [HttpPost("cancel-booking/{id}")]
        public async Task<IActionResult> CancelBooking([FromRoute] int id, [FromBody] int userId)
        {
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

        [HttpPut("create-booking")]
        public async Task<IActionResult> CreateBooking([FromBody] CreateBookingRequest request)
        {
            var response = await _bookingService.CreateBooking(request);

            if (response.Success)
            {
                return Ok(response.Booking);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }

        public async Task<IActionResult> UpdateBooking([FromBody] UpdateBookingRequest request)
        {
            var response = await _bookingService.UpdateBooking(request);

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
