﻿using Models;
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

            var allProduct = await _productRepo.GetAllAsync();
            if (!allProduct.Any(c => c.Id == selectedProductDto.ProductId))
            {
                throw new Exception("Product does not exist");
            }

            var selectedProduct = new SelectedProduct
            {
                Quantity = selectedProductDto.Quantity,
                ProductPrice = selectedProductDto.ProductPrice,
                ProductId = selectedProductDto.ProductId,
                BookingId = selectedProductDto.BookingId,   
            };
            return  selectedProduct;
        }

        public async Task<SelectedProduct> UpdateSelectedProductAsync(int id, SelectedProductDto selectedProductDto)
        {
            var allbooking = await _bookingRepo.GetAllAsync();
            if (!allbooking.Any(c => c.Id == selectedProductDto.BookingId))
            {
                throw new Exception("Booking does not exist");
            }

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
            existingSelectedProduct.Quantity = selectedProductDto.Quantity;
            existingSelectedProduct.ProductPrice = selectedProductDto.ProductPrice;
            existingSelectedProduct.ProductId = selectedProductDto.ProductId;
            existingSelectedProduct.BookingId = selectedProductDto.BookingId;

            return existingSelectedProduct;
        }
    }
}
