using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class BookingDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateOnly ArrivalDate { get; set; }
        public int SlotPrice { get; set; }

        public int BookingId { get; set; }
        public Booking Booking { get; set; }

        public int SlotId { get; set; }
        public Slot Slot { get; set; }
    }
}
