using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class SelectedProductDto
    {
        public int Quantity { get; set; }
        public int ProductPrice { get; set; }
        public int BookingId { get; set; }
        public int ProductId { get; set; }
    }
}
