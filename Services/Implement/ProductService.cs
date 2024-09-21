using Models.DTOs;
using Models;
using Repositories.Interface;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implement
{
    public class ProductService : IProductService
    {
        private readonly IRepositoryBase<Product> _productRepo;
        public ProductService(IRepositoryBase<Product> productservice)
        {
            _productRepo = productservice;
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

        public async Task AddProductAsync(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }
            await _productRepo.AddAsync(product);
        }

        public async Task<Product> AddProductAsync(ProductDTO productDto)
        {
            var product = new Product
            {
                Name = productDto.Name,
                Price = productDto.Price,
                Description = productDto.Description,
                Status = productDto.Status,
                Quantity = productDto.Quantity,
                Unit = productDto.Unit,
                CategoryId = productDto.CategoryId
            };

            await _productRepo.AddAsync(product);
            return product;
        }

        public async Task<Product> UpdateProductAsync(int id, ProductDTO productDto)
        {
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

