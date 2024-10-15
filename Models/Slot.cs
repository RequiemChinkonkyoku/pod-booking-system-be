using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Slot
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Status { get; set; }
        public DateOnly ArrivalDate { get; set; }
        
        public int? ScheduleId { get; set; }
        public Schedule Schedule { get; set; }

        public int? PodId { get; set; }
        public Pod Pod { get; set; }

        public int? BookingDetailId { get; set; }
        public BookingDetail BookingDetail { get; set; }
    }
}
