using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTOs;
using Services.Implement;
using Services.Interface;

namespace PodBookingSystem.API.Controllers
{
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("get-all-categories")]
        public async Task<ActionResult<List<Category>>> GetAllCategory()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("/get-category-by-id/categoryid/{id}")]
        public async Task<ActionResult<Category>> GetCategoryByID(int id)
        {
            try
            {
                var category = await _categoryService.GetCategoryByIDAsync(id);
                return Ok(category);
            }
            catch (Exception ex) when (ex.Message == "Category not found")
            {
                return NotFound("Category Not Found");
            }
        }

        [HttpPost("add-category")]
        public async Task<ActionResult<Category>> AddCategory([FromBody] CategoryDTO categoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var category = await _categoryService.AddCategoryAsync(categoryDto);
                return Ok(category);
            }
            catch (Exception ex)
            {
                if (ex.Message == "Duplicate category name")
                {
                    return Conflict("Duplicate category name");
                }
                return StatusCode(500, "An error occurred while updating the product.");
            }
        }

        [HttpPut("/update-category-by-id/categorytId/{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryDTO categorydto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updatedCategory = await _categoryService.UpdateCategoryAsync(id, categorydto);
                return Ok(updatedCategory);
            }
            catch (Exception ex) 
            {
                if(ex.Message == "Category Not Found")
                {
                    return NotFound("Category Not Found");
                }
                if (ex.Message == "Duplicate Category Name")
                {
                    return Conflict("Duplicate Category Name");
                }
                return StatusCode(500, "An error occurred while updating the product.");
            }
        }

        [HttpDelete("delete-category-by-id/categoryid/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                await _categoryService.DeleteCategoryAsync(id);
                return Ok("Category deleted successfully."); 
            }
            catch (Exception ex) when (ex.Message == "Category not found")
            {
                return NotFound("Category Not Found");
            }
        }
    }
}
