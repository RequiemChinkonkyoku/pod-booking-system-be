using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class UserOtp
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string OtpCode { get; set; }

        [Required]
        public DateTime ExpirationTime { get; set; }

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

        public bool IsExpiredOrUsed { get; set; } = false;
    }
}
