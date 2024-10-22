using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class VnpayInfoModel
    {
        public string OrderType { get; set; }
        public double Amount { get; set; }
        public int BookingId { get; set; }
        public int UserId { get; set; }
    }
}
