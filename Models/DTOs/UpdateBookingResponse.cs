using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class UpdateBookingResponse
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public Booking Booking { get; set; }
    }
}
