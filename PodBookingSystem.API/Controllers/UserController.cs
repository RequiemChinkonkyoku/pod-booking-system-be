using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTOs;
using Services.Interface;

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
        [Authorize(Roles = "3, 4")]
        public async Task<IActionResult> Get()
        {
            var response = await _userService.GetUsersAsync();
            return Ok(response);
        }

        [HttpPost]
        [Authorize(Roles = "3, 4")]
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

        [HttpGet("{id}")]
        [Authorize(Roles = "3, 4")]
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
        [Authorize(Roles = "3, 4")]
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
        [Authorize(Roles = "3, 4")]
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
    }
}
