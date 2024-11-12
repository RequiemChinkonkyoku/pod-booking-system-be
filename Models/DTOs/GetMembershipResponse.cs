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

        public int Point { get; set; }

        public Membership? CurrentMembership { get; set; }

        public Membership? NextMembership { get; set; }

        public Membership? PreviousMembership { get; set; }
    }
}
