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
        Task<List<SelectedProduct>> GetAllSelectedProductAsync();
        Task<SelectedProduct> GetProductByIDAsync(int id);
        Task<SelectedProduct> CreateSelectedProductAsync(SelectedProductDto selectedProductDto);
        Task<SelectedProduct> UpdateSelectedProductAsync(int id, SelectedProductDto selectedProductDto);
        Task<bool> DeleteSelectedProductAsync(int id);
    }
}
