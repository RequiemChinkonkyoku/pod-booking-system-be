using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class UpdateBookingRequest
    {
        public int BookingId { get; set; }

        public int UserId { get; set; }

        public DateOnly NewArrivalDate { get; set; }

        public List<int> NewSlotIds { get; set; }
    }
}
