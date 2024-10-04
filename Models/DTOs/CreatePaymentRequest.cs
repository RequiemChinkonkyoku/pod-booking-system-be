using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class CreatePaymentRequest
    {
        public string FullName { get; set; }
        public int BookingId { get; set; }
        public string OrderId { get; set; }
        public string OrderInfo { get; set; }
        public int Amount { get; set; }
    }
}
