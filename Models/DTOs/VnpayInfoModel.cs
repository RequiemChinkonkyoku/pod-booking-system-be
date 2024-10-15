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
        public string BookingId { get; set; }
        public string Name { get; set; }
    }
}
