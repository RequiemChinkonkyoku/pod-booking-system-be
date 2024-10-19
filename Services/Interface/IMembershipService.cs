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
        public Task<MembershipServiceResponse> GetMembershipById(int id);
        public Task<MembershipServiceResponse> CreateMembership(CreateMembershipRequest request);
        public Task<MembershipServiceResponse> UpdateMembership(int id, UpdateMembershipRequest request);
        public Task<MembershipServiceResponse> ToggleMembership(int id);
        public Task<MembershipServiceResponse> SignUp(int id, int userId);
        public Task<MembershipServiceResponse> CancelMembership(int userId);
    }
}
