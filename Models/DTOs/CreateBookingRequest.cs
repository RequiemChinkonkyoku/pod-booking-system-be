using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class CreateBookingRequest
    {
        public DateOnly ArrivalDate { get; set; }

        public int PodId { get; set; }

        public List<int> ScheduleIds { get; set; }
    }
}
