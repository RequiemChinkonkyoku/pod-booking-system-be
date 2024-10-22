using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Models.DTOs;
using Services.Interface;
using System.Security.Claims;

namespace PodBookingSystem.API.Controllers
{
    [Route("[controller]")]
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

        [HttpPost]
        public async Task<IActionResult> CreatePaymentUrl([FromBody] VnpayInfoModel request)
        {
            var url = await _vnpService.CreatePaymentUrl(request, HttpContext);

            if (url.IsNullOrEmpty())
            {
                return BadRequest("There has been a problem creating paymentUrl");
            }

            return Ok(url);
        }

        [HttpGet("payment-callback")]
        public async Task<IActionResult> PaymentCallBack()
        {
            var response = await _vnpService.PaymentExecute(Request.Query);

            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }
    }
}
