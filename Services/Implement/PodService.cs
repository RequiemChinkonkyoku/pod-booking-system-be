﻿using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Models;
using Models.DTOs;
using Repositories;
using Repositories.Interface;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implement
{
    public class PodService : IPodService
    {
        private readonly IRepositoryBase<Pod> _podRepo;
        private readonly IRepositoryBase<Area> _areaRepo;
        private readonly IRepositoryBase<PodType> _podTypeRepo;
        private readonly IRepositoryBase<Slot> _slotRepo;
        private readonly IRepositoryBase<Schedule> _scheduleRepo;
        private readonly IRepositoryBase<Booking> _bookingRepo;
        private readonly IRepositoryBase<BookingDetail> _bookingDetailRepo;

        public PodService(IRepositoryBase<Pod> podRepo , IRepositoryBase<Area> areaRepo, IRepositoryBase<PodType> podTypeRepo, IRepositoryBase<Slot> slotRepo, IRepositoryBase<Schedule> scheduleRepo, IRepositoryBase<Booking> bookingRepo, IRepositoryBase<BookingDetail> bookingDetailRepo)
        {
            _podRepo = podRepo;
            _areaRepo = areaRepo;
            _podTypeRepo = podTypeRepo;
            _slotRepo = slotRepo;
            _scheduleRepo = scheduleRepo;
            _bookingRepo = bookingRepo;
            _bookingDetailRepo = bookingDetailRepo;
        }

        public async Task<List<Pod>> GetAllPodsAsync()
        {
            return await _podRepo.GetAllAsync();
        }


        public async Task<Pod> GetPodByIDAsync(int id)
        {
            var pod = await _podRepo.FindByIdAsync(id);
            if (pod == null)
            {
                throw new Exception("Pod not Found");
            }
            return pod;
        }

        public async Task<Pod> AddPodAsync(PodDto podDto)
        {
            var  allAreas = await _areaRepo.GetAllAsync();
            if (!allAreas.Any(c => c.Id == podDto.AreaId))
            {
                throw new Exception("Area does not exist");
            }

            var allPods = await _podRepo.GetAllAsync();
            if (allPods.Any(p => p.Name == podDto.Name))
            {
                throw new Exception("Duplicate Pod name");
            }

            var allPodTypes = await _podTypeRepo.GetAllAsync();
            if (!allPodTypes.Any(c => c.Id == podDto.PodTypeId)) 
            {
                throw new Exception("Pod Type does not exist");
            }

            var pod = new Pod
            {
                Name = podDto.Name,
                Description = podDto.Description,
                Status = 1,
                AreaId = podDto.AreaId,
                PodTypeId = podDto.PodTypeId,
            };

            await _podRepo.AddAsync(pod);
            return pod;

        }

        public async Task<Pod> UpdatePodAsync(int id, PodDto podDto)
        {
            var existingPod = await _podRepo.FindByIdAsync(id);
            if (existingPod == null)
            {
                throw new Exception("Pod not found");
            }

            var allAreas = await _areaRepo.GetAllAsync();
            if (!allAreas.Any(c => c.Id == podDto.AreaId))
            {
                throw new Exception("Area does not exist");
            }

            var allPodTypes = await _podTypeRepo.GetAllAsync();
            if (!allPodTypes.Any(c => c.Id == podDto.PodTypeId))
            {
                throw new Exception("Pod Type does not exist");
            }


            var allProducts = await _podRepo.GetAllAsync();
            if (allProducts.Any(p => p.Name == podDto.Name && p.Id != id))
            {
                throw new Exception("Duplicate Pod name");
            }
            existingPod.Name = podDto.Name;
            existingPod.Description = podDto.Description;
            existingPod.Status = podDto.Status;
            existingPod.AreaId = podDto.AreaId;
            existingPod.PodTypeId = podDto.PodTypeId;

            await _podRepo.UpdateAsync(existingPod);
            return existingPod;
        }

        public async Task<Pod> DeletePodAsync(int podId)
        {
            var existingPod = await _podRepo.FindByIdAsync(podId);
            if (existingPod == null)
            {
                throw new Exception("Pod not found");
            }
            var allBookings = await _bookingRepo.GetAllAsync();

            var allSlots = await _slotRepo.GetAllAsync();
            var allBookingDetails = await _bookingDetailRepo.GetAllAsync();
            var podSlots = (await _slotRepo.GetAllAsync())
                .Where(s => s.PodId == podId)
                .Select(s => s.Id)
                .ToList();

            var relatedBookingDetails = (await _bookingDetailRepo.GetAllAsync())
                .Where(bd => podSlots.Contains(bd.SlotId))
                .ToList();

            var hasIncompleteBooking = relatedBookingDetails
            .Any(bd => allBookings.Any(b => b.Id == bd.BookingId && b.BookingStatusId != 5));


            if (hasIncompleteBooking)
            {
                existingPod.Status = 2;
                await _podRepo.UpdateAsync(existingPod);
                return existingPod;
            }
            existingPod.Status = 0;

            await _podRepo.UpdateAsync(existingPod);
            return existingPod;
        }

        public async Task<List<Pod>> GetAvailablePodsByPodTypeAsync(int podTypeId, int scheduleId, DateOnly arrivalDate)
        {
            var allPods = await _podRepo.GetAllAsync();

            var allSlots = await _slotRepo.GetAllAsync();

            var availablePods = allPods
                .Where(p => p.PodTypeId == podTypeId &&
                    p.Status == 1 && 
                    !allSlots.Any(s => s.PodId == p.Id &&
                                       s.ScheduleId == scheduleId &&
                                       s.ArrivalDate == arrivalDate &&
                                       s.Status != 0)) 
                .ToList();

            return availablePods;
        }
    }
}
