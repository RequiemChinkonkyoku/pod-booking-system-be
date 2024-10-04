using Models;
using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IPodTypeService
    {
        public Task<List<PodType>> GetAllPodTypesAsync();
        public Task<PodType> GetPodTypeByIDAsync(int id);
        public Task<PodType> AddPodTypeAsync(PodTypeDto podTypeDto);
        public Task<PodType> UpdatePodTypeAsync(int id, PodTypeDto podTypeDto);
    }
}
