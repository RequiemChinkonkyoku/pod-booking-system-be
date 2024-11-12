using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class CreateReviewRequest
    {
        public int Rating { get; set; }
        public string Text { get; set; }
        public int BookingId { get; set; }
    }
}
