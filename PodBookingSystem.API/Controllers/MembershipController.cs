using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTOs;
using Repositories.Interface;
using Services.Interface;
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

        [HttpGet("/{id}")]
        public async Task<IActionResult> GetMembershipById([FromRoute] int id)
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

            var response = await _memberService.GetMembershipById(id);

            return Ok(response);
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

            return Ok(response);
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
    }
}
