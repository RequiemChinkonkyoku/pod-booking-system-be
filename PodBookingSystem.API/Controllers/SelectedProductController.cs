using Microsoft.AspNetCore.Mvc;
using Models.DTOs;
using Services.Interface;

namespace PodBookingSystem.API.Controllers
{
    [ApiController]
    [Route("/SelectedProduct")]
    public class SelectedProductController : ControllerBase
    {
        private readonly ISelectedProductService _selectedProductService;
        public SelectedProductController(ISelectedProductService selectedProductService)
        {
            _selectedProductService = selectedProductService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSelectedProduct()
        {
            var allSelectedProduct = await _selectedProductService.GetAllSelectedProductAsync();
            return Ok(allSelectedProduct);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSelectedProductByID(int id)
        {
            try
            {
                var selectedProduct = await _selectedProductService.GetProductByIDAsync(id);
                return Ok(selectedProduct);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateSelectedProduct([FromBody] SelectedProductDto selectedProductDto)
        {
            try
            {
                var selectedProduct = await _selectedProductService.CreateSelectedProductAsync(selectedProductDto);
                return Ok(selectedProduct);
            }
            catch(Exception ex) 
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSelectedProduct([FromBody] UpdateSelectedProductDto selectedProductDto, int id)
        {
            try
            {
                var selectedProduct = await _selectedProductService.UpdateSelectedProductAsync(id, selectedProductDto);
                return Ok(selectedProduct);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSelectedProduct([FromRoute] int id)
        {
            try
            {
                var selectedProduct = await _selectedProductService.DeleteSelectedProductAsync(id);
                return Ok(selectedProduct);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("Booking/{id}")]
        public async Task<IActionResult> GetSelecetedProductByBookingID(int id)
        {
            try
            {
                var selectedProduct = await _selectedProductService.GetSelectedProductByBookingID(id);
                return Ok(selectedProduct);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
