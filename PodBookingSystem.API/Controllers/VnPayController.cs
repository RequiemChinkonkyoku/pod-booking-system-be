using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Models.DTOs;
using Services.Interface;

namespace PodBookingSystem.API.Controllers
{
    public class VnPayController : Controller
    {
        private readonly IVnPayService _vnpService;
        private readonly IBookingService _bookingService;
        private readonly ITransactionService _transactionService;

        public VnPayController(IVnPayService vnpService,
                               IBookingService bookingService,
                               ITransactionService transactionService)
        {
            _vnpService = vnpService;
            _bookingService = bookingService;
            _transactionService = transactionService;
        }

        public IActionResult CreatePayment([FromBody] CreatePaymentRequest request)
        {
            var url = _vnpService.CreatePaymentUrl(request, HttpContext);

            if (url.IsNullOrEmpty())
            {
                return BadRequest("There has been a problem creating paymentUrl");
            }

            return Ok(url);
        }

    }
}
