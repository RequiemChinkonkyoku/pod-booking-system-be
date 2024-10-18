using Models;
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
        public Task<List<Slot>> GetFullyBookedSlotByPodTypeAsync(int podTypeId);

    }
}
