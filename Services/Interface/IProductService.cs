using Models.DTOs;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IproductService
    {
        public Task<List<Product>> GetAllProductsAsync();

        public Task<Product> GetProductByIdAsync(int id);

        public Task<Product> AddProductAsync(ProductDTO productDto);

        public Task<Product> UpdateProductAsync(int id, ProductDTO productDto);

        public Task<Product> DeleteProductAsync(int id);
    }
}
