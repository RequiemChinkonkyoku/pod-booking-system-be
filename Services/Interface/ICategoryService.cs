using Models;
using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface ICategoryService
    {
        public Task<List<Category>> GetAllCategoriesAsync();
        public Task<Category> GetCategoryByIdAsync(int id);
        public Task<Category> AddCategoryAsync(CategoryDTO categoryDto);
        public Task<Category> UpdateCategoryAsync(int id, CategoryDTO categoryDTO);
        public Task DeleteCategoryAsync(int id);
    }
}
