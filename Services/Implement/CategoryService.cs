using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Models;
using Models.DTOs;
using Repositories.Interface;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implement
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepositoryBase<Category> _categoryRepo;
        private readonly IRepositoryBase<Product> _productRepo;
        public CategoryService(IRepositoryBase<Category> categoryRepo, IRepositoryBase<Product> productRepo)
        {
            _categoryRepo = categoryRepo;
            _productRepo = productRepo;
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepo.GetAllAsync();
            //var allProducts = await _productRepo.GetAllAsync();
            //AssignProductToCategory(allProducts, categories);
            //categories = categories.Where(category => category.Products != null && category.Products.Any()).ToList();
            return categories;
        }
        private void AssignProductToCategory(List<Product> products, List<Category> category)
        {
            foreach (var categories in category)
            {
                categories.Products = products.Where(p => p.CategoryId == categories.Id).ToList();
            }
        }
        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            var category = await _categoryRepo.FindByIdAsync(id);
            if(category == null)
            {
                throw new Exception("Category not found");
            }
            return category;
        }

        public async Task<Category> AddCategoryAsync(CategoryDTO categoryDto)
        {
            var allCategory = await _categoryRepo.GetAllAsync();

            if (allCategory.Any(p => p.Name == categoryDto.Name))
            {
                throw new Exception("Duplicate category name");
            }
            var category = new Category()
            {
                Name = categoryDto.Name,
            };

            await _categoryRepo.AddAsync(category);
            return category;
        }

        public async Task<Category> UpdateCategoryAsync(int id,[FromBody] CategoryDTO categoryDTO)
        {
            var category = await _categoryRepo.FindByIdAsync(id);
            if(category == null)
            {
                throw new Exception("Category Not Found");
            }
            var allCategory = await _categoryRepo.GetAllAsync();
            if (allCategory.Any(p => p.Name == categoryDTO.Name && p.Id != id))
            {
                throw new Exception("Duplicate Category Name");
            }
            category.Name = categoryDTO.Name;

            await _categoryRepo.UpdateAsync(category);
            return category;
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _categoryRepo.FindByIdAsync(id);
            if(category == null)
            {
                throw new Exception("Category Not Found");

            }
            await _categoryRepo.DeleteAsync(category);
        }
    }
}
