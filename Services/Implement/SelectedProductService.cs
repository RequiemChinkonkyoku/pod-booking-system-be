using Models;
using Repositories.Interface;
using Services.Interface;
using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implement
{
    public class SelectedProductService : ISelectedProductService
    {
        private readonly IRepositoryBase<SelectedProduct> _selectedProductRepo;
        private readonly IRepositoryBase<Product> _productRepo;
        private readonly IRepositoryBase<Booking> _bookingRepo;
        public SelectedProductService(IRepositoryBase<SelectedProduct> selectedProductRepo, IRepositoryBase<Product> productRepo, IRepositoryBase<Booking> bookingRepo)
        {
            _selectedProductRepo = selectedProductRepo;
            _productRepo = productRepo;
            _bookingRepo = bookingRepo;

        }

        public async Task<List<SelectedProduct>> GetAllSelectedProductAsync()
        {
            return await _selectedProductRepo.GetAllAsync();
        }

        public async Task<SelectedProduct> GetProductByIDAsync(int id)
        {

            var selectedProduct = await _selectedProductRepo.FindByIdAsync(id);
            if (selectedProduct == null)
            {
                throw new Exception("Selected Product NotFound");
            }
            return selectedProduct;
        }

        public async Task<SelectedProduct> CreateSelectedProductAsync(SelectedProductDto selectedProductDto)
        {
            var allbooking = await _bookingRepo.GetAllAsync();
            if (!allbooking.Any(c => c.Id == selectedProductDto.BookingId))
            {
                throw new Exception("Booking does not exist");
            }

            var product = await _productRepo.FindByIdAsync(selectedProductDto.ProductId);
            if (product == null)
            {
                throw new Exception("Product does not exist");
            }
            if (product.Quantity < selectedProductDto.Quantity)
            {
                throw new Exception("Insufficient product stock");
            }

            var existingSelectedProduct = (await _selectedProductRepo.GetAllAsync())
                .FirstOrDefault(sp => sp.ProductId == selectedProductDto.ProductId && sp.BookingId == selectedProductDto.BookingId);

            if (existingSelectedProduct != null)
            {
                existingSelectedProduct.Quantity += selectedProductDto.Quantity;
                await _selectedProductRepo.UpdateAsync(existingSelectedProduct);
            }
            else
            {
                var selectedProduct = new SelectedProduct
                {
                    Quantity = selectedProductDto.Quantity,
                    ProductPrice = product.Price,
                    ProductId = selectedProductDto.ProductId,
                    BookingId = selectedProductDto.BookingId,
                };

                await _selectedProductRepo.AddAsync(selectedProduct);
                existingSelectedProduct = selectedProduct;
            }

            product.Quantity -= selectedProductDto.Quantity;
            await _productRepo.UpdateAsync(product);

            return existingSelectedProduct;
        }

        public async Task<SelectedProduct> UpdateSelectedProductAsync(int id, UpdateSelectedProductDto selectedProductDto)
        {
            var allbooking = await _bookingRepo.GetAllAsync();

            var allProduct = await _productRepo.GetAllAsync();
            if (!allProduct.Any(c => c.Id == selectedProductDto.ProductId))
            {
                throw new Exception("Product does not exist");
            }
            var existingSelectedProduct = await _selectedProductRepo.FindByIdAsync(id);
            if (existingSelectedProduct == null)
            {
                throw new Exception("Selected Product NotFound");
            }
            
            var product = await _productRepo.FindByIdAsync(selectedProductDto.ProductId);
            if(selectedProductDto.Quantity > existingSelectedProduct.Quantity)
            {
                product.Quantity -= selectedProductDto.Quantity - existingSelectedProduct.Quantity;
                await _productRepo.UpdateAsync(product);
            }
            if (selectedProductDto.Quantity < existingSelectedProduct.Quantity)
            {
                product.Quantity += existingSelectedProduct.Quantity - selectedProductDto.Quantity;
                await _productRepo.UpdateAsync(product);
            }
            existingSelectedProduct.Quantity = selectedProductDto.Quantity;
            existingSelectedProduct.ProductPrice = product.Price;
            existingSelectedProduct.ProductId = selectedProductDto.ProductId;

            await _selectedProductRepo.UpdateAsync(existingSelectedProduct);

            return existingSelectedProduct;
        }

        public async Task<bool> DeleteSelectedProductAsync(int id)
        {
            bool result = false;
            var selectedProduct = await _selectedProductRepo.FindByIdAsync(id);
            var product = await _productRepo.FindByIdAsync(selectedProduct.ProductId);

            if (selectedProduct != null)
            {
                product.Quantity += selectedProduct.Quantity;
                await _productRepo.UpdateAsync(product);
                await _selectedProductRepo.DeleteAsync(selectedProduct);
                result = true;
            }


            return result;
        }

        public async Task<List<SelectedProduct>> GetSelectedProductByBookingID(int id)
        {
            var products = await _selectedProductRepo.GetAllAsync();
            if (products == null)
            {
                throw new Exception("Selected Product does not exist");
            }
            var filteredProduct = products.Where(p => p.BookingId == id).ToList();

            return filteredProduct;
        }
    }
}
