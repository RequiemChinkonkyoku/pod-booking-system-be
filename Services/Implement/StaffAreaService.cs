using Models;
using Models.DTOs;
using Repositories.Implement;
using Repositories.Interface;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implement
{
    public class StaffAreaService : IStaffAreaService
    {
        private readonly IRepositoryBase<StaffArea> _staffAreaRepo;
        private readonly IRepositoryBase<Area> _areaRepo;
        private readonly IRepositoryBase<User> _userRepo;
        public StaffAreaService(IRepositoryBase<StaffArea> staffAreaRepo, IRepositoryBase<Area> areaRepo, IRepositoryBase<User> userRepo)
        {
                _staffAreaRepo = staffAreaRepo;
                _areaRepo = areaRepo;
                _userRepo = userRepo;
        }

        public async Task<StaffArea> AssignStaffAreaAsync(AssignStaffAreaDto assignStaffAreaDto)
        {
            var allAreas = await _areaRepo.GetAllAsync();
            if (!allAreas.Any(c => c.Id == assignStaffAreaDto.AreaId))
            {
                throw new Exception("Area does not exist");
            }
            var allUser = await _userRepo.GetAllAsync();
            if (!allUser.Any(c => c.Id == assignStaffAreaDto.StaffId && c.RoleId == 2))
            {
                throw new Exception("Staff does not exist");
            }

            var staffArea = new StaffArea
            {
                StaffId = assignStaffAreaDto.StaffId,
                AreaId = assignStaffAreaDto.AreaId,
            };

            await _staffAreaRepo.AddAsync(staffArea);
            return staffArea;
        }

        public async Task<StaffArea> UpdateStaffAreaAsync(int id ,AssignStaffAreaDto assignStaffAreaDto)
        {
            var allAreas = await _areaRepo.GetAllAsync();
            if (!allAreas.Any(c => c.Id == assignStaffAreaDto.AreaId))
            {
                throw new Exception("Area does not exist");
            }
            var allUser = await _userRepo.GetAllAsync();
            if (!allUser.Any(c => c.Id == assignStaffAreaDto.StaffId && c.RoleId == 2))
            {
                throw new Exception("Staff does not exist");
            }
            if (allUser.Any(u => u.Id == assignStaffAreaDto.StaffId && u.Id != id))
            {
                throw new Exception("Staff has been assign to different area");
            }
            var existingStaffArea = await _staffAreaRepo.FindByIdAsync(id);
            if (existingStaffArea == null)
            {
                throw new Exception("Area not found");
            }
            existingStaffArea.StaffId = assignStaffAreaDto.StaffId;
            existingStaffArea.AreaId = assignStaffAreaDto.AreaId;

            await _staffAreaRepo.UpdateAsync(existingStaffArea);

            return existingStaffArea;

        }

    }
}
