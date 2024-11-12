using Microsoft.AspNetCore.Mvc;
using Services.Interface;
using System.Threading.Tasks;
using Models;
using System;
using Models.DTOs;

namespace PodBookingSystem.API.Controllers
{
    [ApiController]
    [Route("/Reviews")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateReview([FromBody] CreateReviewRequest request)
        {
            try
            {
                var createdReview = await _reviewService.CreateReviewAsync(request);
                return CreatedAtAction(nameof(GetReviewById), new { id = createdReview.Id }, createdReview);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the review.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetReviews()
        {
            try
            {
                var review = await _reviewService.GetReviewsAsync();
                if (review == null)
                {
                    return NotFound("Reviews not found.");
                }
                return Ok(review);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving the review.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReviewById(int id)
        {
            try
            {
                var review = await _reviewService.GetReviewByIdAsync(id);
                if (review == null)
                {
                    return NotFound("Review not found.");
                }
                return Ok(review);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving the review.");
            }
        }

        [HttpGet("Booking/{id}")]
        public async Task<IActionResult> GetReviewByBookingId(int id)
        {
            try
            {
                var review = await _reviewService.GetReviewByBookingIdAsync(id);
                if (review == null)
                {
                    return NotFound("Review not found.");
                }
                return Ok(review);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving the review.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReview(int id, [FromBody] CreateReviewRequest request)
        {
            try
            {
                //if (id != review.Id)
                //{
                //    return BadRequest("Review ID mismatch.");
                //}

                var updatedReview = await _reviewService.UpdateReviewAsync(id, request);
                if (updatedReview == null)
                {
                    return NotFound("Review not found.");
                }

                return Ok(updatedReview);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the review.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            try
            {
                var deleted = await _reviewService.DeleteReviewAsync(id);
                if (!deleted)
                {
                    return NotFound("Review not found.");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the review.");
            }
        }
    }
}
