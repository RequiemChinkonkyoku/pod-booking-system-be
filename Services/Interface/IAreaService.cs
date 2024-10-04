using Models;
using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IAreaService
    {
        public Task<List<Area>> GetAllAreasAsync();
        public Task<Area> GetAreaByIDAsync(int id);
        public Task<Area> AddAreaAsync(AreaDto areaDto);
        public Task<Area> UpdateAreaAsync(int id, AreaDto areaDto);

    }
}
