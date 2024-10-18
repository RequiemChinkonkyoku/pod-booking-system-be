using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class GetMembershipResponse
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public Membership Membership { get; set; }
    }
}
