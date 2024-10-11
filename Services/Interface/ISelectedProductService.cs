using Models;
using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface ISelectedProductService
    {
        public Task<List<SelectedProduct>> GetAllSelectedProductAsync();
        public Task<SelectedProduct> GetProductByIDAsync(int id);
        public Task<SelectedProduct> CreateSelectedProductAsync(SelectedProductDto selectedProductDto);
        public Task<SelectedProduct> UpdateSelectedProductAsync(int id, SelectedProductDto selectedProductDto);
    }
}
