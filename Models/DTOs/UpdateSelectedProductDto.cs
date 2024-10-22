using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class UpdateSelectedProductDto
    {
        public int Quantity { get; set; }
        public int ProductId { get; set; }
    }
}
