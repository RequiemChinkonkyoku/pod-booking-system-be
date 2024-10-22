using Microsoft.Extensions.Configuration;
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
    public class ScheduleService : IScheduleService
    {
        private readonly IRepositoryBase<Schedule> _scheduleRepo;

        public ScheduleService(IRepositoryBase<Schedule> scheduleRepo)
        {
            _scheduleRepo = scheduleRepo;
        }

        public async Task<List<Schedule>> GetAllSchedulesAsync()
        {
            var schedules = await _scheduleRepo.GetAllAsync();
            return schedules;
        }

        public async Task<Schedule> GetSchedulesByIds(List<int> scheduleIdList)
        {
            Schedule result = new Schedule
            {
                StartTime = TimeOnly.MaxValue,
                EndTime = TimeOnly.MinValue
            };
            foreach (var scheduleId in scheduleIdList)
            {
                var schedule = await _scheduleRepo.FindByIdAsync(scheduleId);
                if (schedule == null)
                {
                    return null;
                }
                if (schedule.StartTime < result.StartTime)
                {
                    result.StartTime = schedule.StartTime;
                }
                if (schedule.EndTime > result.EndTime)
                {
                    result.EndTime = schedule.EndTime;
                }
            }
            return result;
        }
    }
}
