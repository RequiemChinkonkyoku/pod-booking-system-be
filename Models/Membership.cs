using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Membership
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int PointsRequirement { get; set; }
        public int Discount { get; set; }
        public int Status { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<Booking> Bookings { get; set; }
    }
}
