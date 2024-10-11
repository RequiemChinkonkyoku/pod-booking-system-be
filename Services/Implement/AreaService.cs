using Models;
using Models.DTOs;
using Repositories.Interface;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implement
{
    public class AreaService : IAreaService
    {
        private readonly IRepositoryBase<Area> _areaRepo;
        private readonly IRepositoryBase<Pod> _podRepo;
        public AreaService(IRepositoryBase<Area> areaRepo, IRepositoryBase<Pod> podRepo)
        {
            _areaRepo = areaRepo;
            _podRepo = podRepo;
        }

        public async Task<List<Area>> GetAllAreasAsync()
        {
            var areas = await _areaRepo.GetAllAsync();

            var allPods = await _podRepo.GetAllAsync();

            AssignPodsToAreas(areas, allPods);

            areas = areas.Where(area => area.Pods != null && area.Pods.Any()).ToList();

            return areas;

        }

        private void AssignPodsToAreas(List<Area> areas, List<Pod> pods)
        {
            foreach (var area in areas)
            {
                area.Pods = pods.Where(p => p.AreaId == area.Id).ToList();
            }
        }

        public async Task<Area> GetAreaByIDAsync(int id)
        {
            var area = await _areaRepo.FindByIdAsync(id);
            if (area == null) 
            {
                throw new Exception("Area not Found");
            }
            return area;
        }

        public async Task<Area> AddAreaAsync(AreaDto areaDto)
        {
            var area = new Area
            {
                Name = areaDto.Name,
                Description = areaDto.Description,
                Location = areaDto.Location

            };
            await _areaRepo.AddAsync(area);
            return area;
        }

        public async Task<Area> UpdateAreaAsync(int id, AreaDto areaDto)
        {
            var existingArea = await _areaRepo.FindByIdAsync(id);
            if (existingArea == null)
            {
                throw new Exception("Area not Found");
            }
            existingArea.Name = areaDto.Name;
            existingArea.Description = areaDto.Description;
            existingArea.Location = areaDto.Location;

            await _areaRepo.UpdateAsync(existingArea); 
            return existingArea;
        }
    }
}
