using Microsoft.AspNetCore.Mvc;
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
        private readonly ITransactionService _transactionService;

        public MomoController(IMomoService momoService, IBookingService bookingService, ITransactionService transactionService)
        {
            _momoService = momoService;
            _bookingService = bookingService;
            _transactionService = transactionService;
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
            request.Amount = getResponse.Booking.BookingPrice;

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

        [HttpPost("payment-execute/{id}")]
        public async Task<IActionResult> PaymentExecuteAsync(int id)
        {
            if (id == null)
            {
                return BadRequest("There has been an error during the payment process");
            }

            var updateResponse = await _bookingService.UpdateBookingStatus(id);

            _transactionService.CreateTransaction(id, updateResponse.Success);

            if (updateResponse.Success)
            {
                return Ok(updateResponse);
            }
            else
            {
                return BadRequest(updateResponse.Message);
            }

        }
    }
}
