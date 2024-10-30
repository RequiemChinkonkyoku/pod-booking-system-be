using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class MomoExecuteResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string ErrorCode { get; set; }
        public int BookingId { get; set; }
        public string OrderId { get; set; }
        public int Amount { get; set; }
        public string OrderInfo { get; set; }
    }
}
