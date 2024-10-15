using Models;
using Repositories.Interface;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implement
{
    public class SlotService : ISlotService
    {
        private readonly IRepositoryBase<Slot> _slotRepo;
        private readonly IRepositoryBase<Pod> _podRepo;

        public SlotService(IRepositoryBase<Slot> slotRepo, IRepositoryBase<Pod> podRepo)
        {
            _slotRepo = slotRepo;
            _podRepo = podRepo;

        }


        public async Task<List<Slot>> GetSlotBySlotTypeAsync(int id)
        {
            var allPods = await _podRepo.GetAllAsync(); 
            var relevantPodIds = allPods
                .Where(p => p.PodTypeId == id)
                .Select(p => p.Id)
                .ToList();
            var allSlots = await _slotRepo.GetAllAsync();
            
            var filteredSlots = allSlots
                .Where(s => s.PodId.HasValue && relevantPodIds.Contains(s.PodId.Value)) 
                .ToList();

            return filteredSlots;
        }
    }
}
