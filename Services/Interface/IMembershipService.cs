using Models;
using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IMembershipService
    {
        public Task<List<Membership>> ViewAllMembership();
        public Task<GetMembershipResponse> GetMembershipById(int id);
        public Task<CreateMembershipResponse> CreateMembership(CreateMembershipRequest request);
        public Task<MembershipSignUpResponse> SignUp(int id, int userId);
    }
}
