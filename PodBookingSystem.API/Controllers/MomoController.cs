using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Models.DTOs;
using Repositories.Implement;
using Services.Implement;
using Services.Interface;
using System.Security.Claims;

namespace PodBookingSystem.API.Controllers
{
    [Route("[controller]")]
    public class MomoController : Controller
    {
        private readonly IMomoService _momoService;
        private readonly IBookingService _bookingService;

        public MomoController(IMomoService momoService, IBookingService bookingService)
        {
            _momoService = momoService;
            _bookingService = bookingService;
        }

        [HttpPost("create-payment")]
        public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentRequest request)
        {
            var getResponse = await _bookingService.GetBookingById(request.BookingId);

            if (getResponse.Success == false)
            {
                return BadRequest(getResponse.Message);
            }

            request.FullName = getResponse.Booking.User.Name;
            //request.Amount = getResponse.Booking.BookingPrice;

            var response = await _momoService.CreatePaymentAsync(request);

            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }

        [HttpPost("payment-execute")]
        public async Task<IActionResult> PaymentExecute()
        {
            var query = HttpContext.Request.Query;

            if (query == null)
            {
                return BadRequest("There has been an error during the payment process");
            }

            var response = await _momoService.PaymentExecute(query);

            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }
    }
}
