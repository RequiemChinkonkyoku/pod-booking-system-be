using Models;
using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IPodService
    {
        public Task<List<Pod>> GetAllPodsAsync();
        public Task<Pod> GetPodByIDAsync(int id);
        public Task<Pod> AddPodAsync(PodDto podDto);
        public Task<Pod> UpdatePodAsync(int id, PodDto podDto);
        public Task<Pod> DeletePodAsync(int podId);
    }
}
