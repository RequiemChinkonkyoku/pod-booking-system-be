using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Models;
using Models.DTOs;
using Repositories.Interface;
using Services.Interface;
using System.Net.WebSockets;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;

namespace PodBookingSystem.API.Controllers
{
    [Route("[controller]")]
    public class MembershipController : Controller
    {
        public readonly IMembershipService _memberService;

        public MembershipController(IMembershipService memberService)
        {
            _memberService = memberService;
        }

        [HttpGet]
        public async Task<IActionResult> ViewAllMembership()
        {
            var response = await _memberService.ViewAllMembership();

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMembershipById([FromRoute] int id)
        {
            var response = await _memberService.GetMembershipById(id);

            if (response.Success)
            {
                return Ok(response.Membership);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }

        [HttpGet("customer")]
        public async Task<IActionResult> GetCustomerMembership()
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

            var response = await _memberService.GetCustomerMembership(userId);

            if (response.Success)
            {
                return Ok(response.Membership);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateMembership([FromBody] CreateMembershipRequest request)
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

            var response = await _memberService.CreateMembership(request);

            if (response.Success)
            {
                return Ok(response.Membership);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMembership([FromRoute] int id, [FromBody] UpdateMembershipRequest request)
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

            var response = await _memberService.UpdateMembership(id, request);

            if (response.Success)
            {
                return Ok(response.Membership);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }

        [HttpPut("toggle/{id}")]
        public async Task<IActionResult> ToggleMembership([FromRoute] int id)
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

            var response = await _memberService.ToggleMembership(id);

            if (response.Success)
            {
                return Ok(response.Membership);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUpForMembership([FromBody] int id)
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

            var response = await _memberService.SignUp(id, userId);

            if (response.Success)
            {
                return Ok(response.Membership);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }

        [HttpPost("cancel-membership")]
        public async Task<IActionResult> CancelMembership()
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

            var response = await _memberService.CancelMembership(userId);

            if (response.Success)
            {
                return Ok(response.Membership);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }

        [HttpGet("get-membership-progress")]
        public async Task<IActionResult> GetMembershipProgress()
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

            var response = await _memberService.GetMembershipProgress(userId);

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
