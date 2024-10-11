using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class UpdateBookingRequest
    {
        public DateOnly NewArrivalDate { get; set; }

        public int NewPodId { get; set; }

        public List<int> NewScheduleIds { get; set; }
    }
}
