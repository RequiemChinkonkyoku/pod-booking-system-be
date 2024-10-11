using Models.DTOs;
using Models;
using Repositories.Interface;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repositories.Implement;

namespace Services.Implement
{
    public class ProductService : IProductService
    {
        private readonly IRepositoryBase<Product> _productRepo;
        private readonly IRepositoryBase<Category> _categoryRepo;

        public ProductService(IRepositoryBase<Product> productService, IRepositoryBase<Category> categoryRepo)
        {
            _productRepo = productService;
            _categoryRepo = categoryRepo;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _productRepo.GetAllAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var product = await _productRepo.FindByIdAsync(id);
            if (product == null)
            {
                throw new Exception("Product not found");
            }
            return product;
        }

        public async Task<Product> AddProductAsync(ProductDTO productDto)
        {
            var allCategories = await _categoryRepo.GetAllAsync(); 
            if (!allCategories.Any(c => c.Id == productDto.CategoryId))
            {
                throw new Exception("Category does not exist");
            }
            var allProducts = await _productRepo.GetAllAsync();

            if (allProducts.Any(p => p.Name == productDto.Name))
            {
                throw new Exception("Duplicate product name");
            }
            var product = new Product
            {
                Name = productDto.Name,
                Price = productDto.Price,
                Description = productDto.Description,
                Status = 1,
                Quantity = productDto.Quantity,
                Unit = productDto.Unit,
                CategoryId = productDto.CategoryId
            };

            await _productRepo.AddAsync(product);
            return product;
        }

        public async Task<Product> UpdateProductAsync(int id, ProductDTO productDto)
        {
            var allCategories = await _categoryRepo.GetAllAsync();
            if (!allCategories.Any(c => c.Id == productDto.CategoryId))
            {
                throw new Exception("Category does not exist");
            }
            var allProducts = await _productRepo.GetAllAsync();
            if (allProducts.Any(p => p.Name == productDto.Name && p.Id != id))
            {
                throw new Exception("Duplicate product name");
            }
            var product = await _productRepo.FindByIdAsync(id);
            if (product == null)
            {
                throw new Exception("Product not found");
            }

            product.Name = productDto.Name;
            product.Price = productDto.Price;
            product.Description = productDto.Description;
            product.Status = productDto.Status;
            product.Quantity = productDto.Quantity;
            product.Unit = productDto.Unit;
            product.CategoryId = productDto.CategoryId;

            await _productRepo.UpdateAsync(product);
            return product;
        }

        public async Task<Product> DeleteProductAsync(int id)
        {
            var product = await _productRepo.FindByIdAsync(id);
            if (product == null)
            {
                throw new Exception("Product not found");
            }

            product.Status = 0;
            await _productRepo.UpdateAsync(product);
            return product;
        }
    }
}

