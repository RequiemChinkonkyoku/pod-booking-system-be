using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Booking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int BookingPrice { get; set; }
        public DateTime CreatedTime { get; set; }

        public int BookingStatusId { get; set; }
        public BookingStatus BookingStatus { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<SelectedProduct> SelectedProducts { get; set; }

        public ICollection<BookingDetail> BookingDetails { get; set; }

        public ICollection<Transaction> Transactions { get; set; }

        public Review Review { get; set; }
    }
}
