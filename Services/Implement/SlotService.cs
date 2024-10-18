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
    public class SlotService : ISlotService
    {
        private readonly IRepositoryBase<Slot> _slotRepo;
        private readonly IRepositoryBase<Pod> _podRepo;
        private readonly IRepositoryBase<Schedule> _scheduleRepo;

        public SlotService(IRepositoryBase<Slot> slotRepo, IRepositoryBase<Pod> podRepo, IRepositoryBase<Schedule> scheduleRepo)
        {
            _slotRepo = slotRepo;
            _podRepo = podRepo;
            _scheduleRepo = scheduleRepo;
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

        public async Task<List<FullyBookedSlotDto>> GetFullyBookedSlotByPodTypeAsync(int podTypeId)
        {
            var allPods = await _podRepo.GetAllAsync();
            var relevantPodIds = allPods
                .Where(p => p.PodTypeId == podTypeId && p.Status == 1)
                .Select(p => p.Id)
                .ToList();

            
            var allSlots = await _slotRepo.GetAllAsync();

            var fullyBookedSlots = allSlots
                .Where(s => s.PodId.HasValue && relevantPodIds.Contains(s.PodId.Value))
                .GroupBy(s => new { s.ScheduleId, s.ArrivalDate }) 
                .Where(g => g.Select(slot => slot.PodId).Distinct().Count() == relevantPodIds.Count)
                .Select(g => new FullyBookedSlotDto
                {
                    ScheduleId = g.Key.ScheduleId,
                    ArrivalDate = g.Key.ArrivalDate 
                })
                .ToList();


            return fullyBookedSlots;

        }
    }
}
