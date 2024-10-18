using Microsoft.IdentityModel.Tokens;
using Models;
using Models.DTOs;
using Repositories.Interface;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implement
{
    public class MembershipService : IMembershipService
    {
        private readonly IRepositoryBase<Membership> _memberRepo;
        private readonly IRepositoryBase<User> _userRepo;

        public MembershipService(IRepositoryBase<Membership> memberRepo, IRepositoryBase<User> userRepo)
        {
            _memberRepo = memberRepo;
            _userRepo = userRepo;

        }

        public async Task<List<Membership>> ViewAllMembership()
        {
            var memberships = await _memberRepo.GetAllAsync();

            return memberships;
        }

        public async Task<GetMembershipResponse> GetMembershipById(int id)
        {
            var membership = await _memberRepo.FindByIdAsync(id);

            if (membership == null)
            {
                return new GetMembershipResponse { Success = false, Message = $"No membership with id {id}" };
            }

            return new GetMembershipResponse { Success = true, Membership = membership };
        }

        public async Task<CreateMembershipResponse> CreateMembership(CreateMembershipRequest request)
        {
            if (request.Name.IsNullOrEmpty() || request.Description.IsNullOrEmpty())
            {
                return new CreateMembershipResponse { Success = false, Message = "All fields must be given." };
            }

            var membership = new Membership { Name = request.Name.Trim(), Description = request.Description.Trim(), Status = 1 };

            try
            {
                await _memberRepo.AddAsync(membership);
            }
            catch (Exception ex)
            {
                return new CreateMembershipResponse { Success = false, Message = "There has been an error creating membership." };
            }

            return new CreateMembershipResponse { Success = true, Membership = membership };
        }

        public async Task<MembershipSignUpResponse> SignUp(int id, int userId)
        {
            if (id <= 0 || userId <= 0)
            {
                return new MembershipSignUpResponse { Success = false, Message = "All fields must be given" };
            }

            var membership = await _memberRepo.FindByIdAsync(id);

            if (membership == null)
            {
                return new MembershipSignUpResponse { Success = false, Message = $"Invalid membershipId {id}" };
            }

            var user = await _userRepo.FindByIdAsync(userId);

            if (user == null)
            {
                return new MembershipSignUpResponse { Success = false, Message = $"Invalid userId {userId}" };
            }

            if (user.MembershipId == id)
            {
                return new MembershipSignUpResponse { Success = false, Message = $"The user has already signup for this membership." };
            }

            user.MembershipId = id;

            try
            {
                await _userRepo.UpdateAsync(user);
            }
            catch (Exception ex)
            {
                return new MembershipSignUpResponse { Success = false, Message = "There has been an error updating user." };
            }

            membership.Users.Add(user);

            return new MembershipSignUpResponse { Success = true, Membership = membership };
        }

        public async Task<ChangemembershipResponse> ChangeMembership(int id, int userId)
        {
            var result = true;

            var user = await _userRepo.FindByIdAsync(userId);

            if (user == null)
            {
                return new ChangemembershipResponse { Success = false, Message = $"Cant find user with id {id}" };
            }

            if (user.MembershipId == 1)
            {
                return new ChangemembershipResponse { Success = false, Message = $"The user membership cannot be changed." };
            }

            var membership = await _memberRepo.FindByIdAsync(id);

            if (membership == null)
            {
                return new ChangemembershipResponse { Success = false, Message = $"Cant find membership with id {id}" };
            }

            user.MembershipId = id;

            try
            {
                await _userRepo.UpdateAsync(user);
            }
            catch (Exception ex)
            {
                return new ChangemembershipResponse { Success = false, Message = $"There has been an error updating the user membership." };
            }

            return new ChangemembershipResponse { Success = true };
        }

        public async Task<bool> CancelMembership(int userId)
        {
            var result = true;

            var user = await _userRepo.FindByIdAsync(userId);

            if (user == null)
            {
                result = false;
            }

            if (user.MembershipId == 1)
            {
                result = false;
            }

            user.MembershipId = 2;

            try
            {
                await _userRepo.UpdateAsync(user);
            }
            catch (Exception ex)
            {
                result = false;
            }

            return result;
        }
    }
}
