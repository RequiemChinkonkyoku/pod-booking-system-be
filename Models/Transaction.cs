using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime PaymentTime { get; set; }
        public int TotalPrice { get; set; }
        public int Status { get; set; }

        public int MethodId { get; set; }
        public Method Method { get; set; }

        public int BookingId { get; set; }
        public Booking Booking { get; set; }

        public string OrderId { get; set; }
        public string PaymentId { get; set; }
    }
}
