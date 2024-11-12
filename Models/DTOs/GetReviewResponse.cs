using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class GetReviewResponse
    {
        public int ReviewId { get; set; }
        public int Rating { get; set; }
        public string Text { get; set; }

        public string CustomerName { get; set; }
        public int PodTypeId { get; set; }
        public string PodTypeName { get; set; }
        public DateOnly ArrivalDate { get; set; }
    }
}
