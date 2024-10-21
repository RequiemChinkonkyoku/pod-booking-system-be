
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class FullyBookedSlotDto
    {
        public int? ScheduleId { get; set; }
        public DateOnly ArrivalDate { get; set; }

    }
}
