using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTOs;
using Repositories.Implement;
using Services.Implement;
using Services.Interface;

namespace PodBookingSystem.API.Controllers
{
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("get-all-products")]
        public async Task<ActionResult<List<Product>>> GetAllProduct()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet("/get-product-by-id/productId/{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);
                return Ok(product);

            }
            catch (Exception ex) when (ex.Message == "Product not found")
            {
                return NotFound("Product Not Found");
            }
        }

        [HttpPost("add-product")]
        public async Task<IActionResult> AddProduct([FromBody] ProductDTO productDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var product = await _productService.AddProductAsync(productDto);
                return Ok(product);
            }
            catch (Exception ex)
            {
                if (ex.Message == "Duplicate product name")
                {
                    return Conflict("Duplicate product name"); 
                }
                if (ex.Message == "Category does not exist")
                {
                    return NotFound("Category does not exist");
                }
                return StatusCode(500, "An error occurred while adding the product.");
            }

        }

        [HttpPut("/update-product-by-id/productId/{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductDTO productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updatedProduct = await _productService.UpdateProductAsync(id, productDto);
                return Ok(updatedProduct);
            }
            catch (Exception ex)
            {
                if (ex.Message == "Duplicate product name")
                {
                    return Conflict(ex.Message); 
                }
                else if (ex.Message == "Category does not exist")
                {
                    return NotFound(ex.Message); 
                }
                else if (ex.Message == "Product not found")
                {
                    return NotFound(ex.Message); 
                }
                return StatusCode(500, "An error occurred while updating the product.");
            }
        }

        [HttpDelete("/delete-product-by-id/productId/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                await _productService.DeleteProductAsync(id);
                return Ok("Product deleted successfully.");
            }
            catch (Exception ex) when (ex.Message == "Product not found")
            {
                return NotFound("Product Not Found");
            }
        }
    }
}
