﻿using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IScheduleService
    {
        Task<List<Schedule>> GetAllSchedulesAsync();
        Task<Schedule> GetSchedulesByIds(List<int> scheduleIdList);
    }
}
