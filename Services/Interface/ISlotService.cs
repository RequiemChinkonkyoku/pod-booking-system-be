using Models;
using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface ISlotService
    {
        public Task<List<Slot>> GetSlotBySlotTypeAsync(int id);
        public Task<List<FullyBookedSlotDto>> GetFullyBookedSlotByPodTypeAsync(int podTypeId);

    }
}
