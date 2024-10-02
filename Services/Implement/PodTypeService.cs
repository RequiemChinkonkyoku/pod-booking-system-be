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
    public class PodTypeService : IPodTypeService
    {
        private readonly IRepositoryBase<PodType> _podTypeRepo;
        public PodTypeService(IRepositoryBase<PodType> podTypeRepo)
        {
            _podTypeRepo = podTypeRepo;
        }

        public async Task<List<PodType>> GetAllPodTypesAsync()
        {
            return await _podTypeRepo.GetAllAsync();
        }

        public async Task<PodType> GetPodTypeByIDAsync(int id)
        {
            var podType = await _podTypeRepo.FindByIdAsync(id);
            if (podType == null)
            {
                throw new Exception("No PodType Found");
            }
            return podType;
        }

        public async Task<PodType> AddPodTypeAsync(PodTypeDto podTypeDto)
        {
            var allPodTypes = await _podTypeRepo.GetAllAsync();
            if (allPodTypes.Any(pt => pt.Name == podTypeDto.Name))
            {
                throw new Exception("Duplicate PodType name");
            }

            var podType = new PodType
            {
                Name = podTypeDto.Name,
                Description = podTypeDto.Description,
                Price = podTypeDto.Price
            };

            await _podTypeRepo.AddAsync(podType);

            return podType;
        }

        public async Task<PodType> UpdatePodTypeAsync(int id, PodTypeDto podTypeDto)
        {
            var existingPodType = await _podTypeRepo.FindByIdAsync(id);
            if (existingPodType == null)
            {
                throw new Exception("PodType not found");
            }

            var allPodTypes = await _podTypeRepo.GetAllAsync();
            if (allPodTypes.Any(pt => pt.Name == podTypeDto.Name && pt.Id != id))
            {
                throw new Exception("Duplicate PodType name");
            }

            existingPodType.Name = podTypeDto.Name;
            existingPodType.Description = podTypeDto.Description;
            existingPodType.Price = podTypeDto.Price;

            await _podTypeRepo.UpdateAsync(existingPodType);

            return existingPodType;
        }


    }
}
