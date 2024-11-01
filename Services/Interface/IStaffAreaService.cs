using Models.DTOs;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IStaffAreaService
    {
        Task<StaffArea> AssignStaffAreaAsync(AssignStaffAreaDto assignStaffAreaDto);
        Task<StaffArea> GetStaffAreaByIdAsync(int id);
        Task<StaffArea> UpdateStaffAreaAsync(/*int id, */AssignStaffAreaDto assignStaffAreaDto);
    }
}
