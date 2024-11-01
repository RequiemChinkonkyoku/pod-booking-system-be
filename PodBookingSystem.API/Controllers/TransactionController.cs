using Microsoft.AspNetCore.Mvc;
using Models;
using Repositories.Interface;
using Services.Interface;
using System.Security.Claims;

namespace PodBookingSystem.API.Controllers
{
    [Route("[controller]")]
    public class TransactionController : Controller
    {
        private readonly ITransactionService _transService;

        public TransactionController(ITransactionService transService)
        {
            _transService = transService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTransaction()
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

            var response = await _transService.GetAllTransaction();

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransactionById([FromRoute] int id)
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

            var response = await _transService.GetTransactionById(id);

            if (response.Success)
            {
                return Ok(response.Transaction);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }

        [HttpGet("booking/{id}")]
        public async Task<IActionResult> GetSuccessTransactionByBookingId([FromRoute] int id)
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

            var response = await _transService.GetSuccessTransactionByBookingId(id);

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
