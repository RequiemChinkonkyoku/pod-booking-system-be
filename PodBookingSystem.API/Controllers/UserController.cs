using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTOs;
using Services.Interface;
using System.Security.Claims;

namespace PodBookingSystem.API.Controllers
{
    [ApiController]
    [Route("/Users")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        //[Authorize(Roles = "3, 4")]
        public async Task<IActionResult> Get()
        {
            var response = await _userService.GetUsersAsync();
            return Ok(response);
        }

        [HttpPost]
        //[Authorize(Roles = "3, 4")]
        public async Task<IActionResult> Post([FromBody] CreateUserDto request)
        {
            try
            {
                var response = await _userService.CreateUserAsync(request);
                return CreatedAtAction(nameof(GetUserById), new { id = response.Id }, response);
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

        [HttpPost("Email")]
        //[Authorize(Roles = "3, 4")]
        public async Task<IActionResult> PostCreateStaff(string email)
        {
            try
            {
                var response = await _userService.CreateStaffAsync(email);
                return CreatedAtAction(nameof(GetUserById), new { id = response.Id }, response);
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

        [HttpGet("{id}")]
        //[Authorize(Roles = "3, 4")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPut("{id}")]
        //[Authorize(Roles = "3, 4")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateUserDto request)
        {
            try
            {
                var response = await _userService.UpdateUserAsync(id, request);
                return Ok(response);
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

        [HttpDelete("{id}")]
        //[Authorize(Roles = "3, 4")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var success = await _userService.DeleteUserAsync(id);
                if (success)
                {
                    return NoContent(); // 204 No Content
                }
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message); // 404 Not Found
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred: " + ex.Message);
            }

            // Fallback
            return StatusCode(500, "An unexpected error occurred.");
        }

        [HttpGet("Current")]
        [Authorize(Roles = "1, 2, 3, 4")]
        public async Task<IActionResult> GetCurrentUser()
        {
            int id = -1;
            try
            {
                id = Int32.Parse(User.FindFirst(ClaimTypes.Name)?.Value.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPut("{id}/Name")]
        //[Authorize(Roles = "1, 2, 3, 4")]
        public async Task<IActionResult> UserUpdateName(int id, string name)
        {
            try
            {
                var response = await _userService.UserUpdateNameAsync(id, name);
                return Ok(response);
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

        [HttpPut("{id}/Password")]
        //[Authorize(Roles = "1, 2, 3, 4")]
        public async Task<IActionResult> UserUpdatePassword(int id, string currentPassword, string newPassword)
        {
            try
            {
                var response = await _userService.UserUpdatePassword(id, currentPassword, newPassword);
                return Ok(response);
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
    }
}
