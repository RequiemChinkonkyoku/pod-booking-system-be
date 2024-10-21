using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class BookingOverviewDto
    {
        public int BookingId { get; set; }
        public DateOnly ArrivalDate { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public int StatusId { get; set; }
        public int PodTypeId { get; set; }
        public string PodTypeName { get; set; }
    }
}
