﻿using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
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

        public async Task<MembershipServiceResponse> GetMembershipById(int id)
        {
            var membership = await _memberRepo.FindByIdAsync(id);

            if (membership == null)
            {
                return new MembershipServiceResponse { Success = false, Message = $"No membership with id {id}" };
            }

            return new MembershipServiceResponse { Success = true, Membership = membership };
        }

        public async Task<MembershipServiceResponse> GetCustomerMembership(int id)
        {
            var user = await _userRepo.FindByIdAsync(id);

            if (user == null)
            {
                return new MembershipServiceResponse { Success = false, Message = "Unable to find user." };
            }

            var membership = await _memberRepo.FindByIdAsync(user.MembershipId.Value);

            if (membership == null)
            {
                return new MembershipServiceResponse { Success = false, Message = "Unable to find membership." };
            }

            return new MembershipServiceResponse { Success = true, Membership = membership };
        }

        public async Task<MembershipServiceResponse> CreateMembership(CreateMembershipRequest request)
        {
            if (request.Name.IsNullOrEmpty() || request.Description.IsNullOrEmpty())
            {
                return new MembershipServiceResponse { Success = false, Message = "All fields must be given." };
            }

            var membership = new Membership
            {
                Name = request.Name.Trim(),
                Description = request.Description.Trim(),
                Status = 1,
                Discount = request.Discount,
                PointsRequirement = request.PointsRequirement
            };

            try
            {
                await _memberRepo.AddAsync(membership);
            }
            catch (Exception ex)
            {
                return new MembershipServiceResponse { Success = false, Message = "There has been an error creating membership." };
            }

            var users = await _userRepo.GetAllAsync();

            var validUsers = users.Where(u => u.LoyaltyPoints >= membership.PointsRequirement);

            if (validUsers.Any())
            {
                foreach (var user in validUsers)
                {
                    user.MembershipId = membership.Id;

                    try
                    {
                        await _userRepo.UpdateAsync(user);
                    }
                    catch (Exception ex)
                    {
                        return new MembershipServiceResponse { Success = false, Message = "Unable to update membership for user " + user.Id };
                    }
                }
            }

            return new MembershipServiceResponse { Success = true, Membership = membership };
        }

        public async Task<MembershipServiceResponse> UpdateMembership(int id, UpdateMembershipRequest request)
        {
            if (id <= 0 || request.Name.IsNullOrEmpty() || request.Description.IsNullOrEmpty())
            {
                return new MembershipServiceResponse { Success = false, Message = "All fields must be given." };
            }

            var membership = await _memberRepo.FindByIdAsync(id);

            if (membership == null)
            {
                return new MembershipServiceResponse { Success = false, Message = $"Invalid membership with id {id}" };
            }

            membership.Name = request.Name.Trim();
            membership.Description = request.Description.Trim();
            membership.Discount = request.Discount;
            membership.PointsRequirement = request.PointsRequirement;

            try
            {
                await _memberRepo.UpdateAsync(membership);
            }
            catch (Exception ex)
            {
                return new MembershipServiceResponse { Success = false, Message = $"There has been an error updating membership." };
            }

            var users = await _userRepo.GetAllAsync();

            var validUsers = users.Where(u => u.LoyaltyPoints >= membership.PointsRequirement);

            if (validUsers.Any())
            {
                foreach (var user in validUsers)
                {
                    user.MembershipId = membership.Id;

                    try
                    {
                        await _userRepo.UpdateAsync(user);
                    }
                    catch (Exception ex)
                    {
                        return new MembershipServiceResponse { Success = false, Message = "Unable to update membership for user " + user.Id };
                    }
                }
            }

            return new MembershipServiceResponse { Success = true, Membership = membership };
        }

        public async Task<MembershipServiceResponse> ToggleMembership(int id)
        {
            if (id == null || id <= 0)
            {
                return new MembershipServiceResponse { Success = false, Message = "MembershipId must be given." };
            }

            var membership = await _memberRepo.FindByIdAsync(id);

            if (membership == null)
            {
                return new MembershipServiceResponse { Success = false, Message = $"There are no membership with id {id}" };
            }

            membership.Status = (membership.Status == 1) ? 0 : 1;

            try
            {
                await _memberRepo.UpdateAsync(membership);
            }
            catch (Exception ex)
            {
                return new MembershipServiceResponse { Success = false, Message = "There has been an error updating the status" };
            }

            return new MembershipServiceResponse { Success = true, Membership = membership };
        }


        public async Task<MembershipServiceResponse> SignUp(int id, int userId)
        {
            if (id <= 0 || userId <= 0)
            {
                return new MembershipServiceResponse { Success = false, Message = "All fields must be given" };
            }

            var membership = await _memberRepo.FindByIdAsync(id);

            if (membership == null || id == 1)
            {
                return new MembershipServiceResponse { Success = false, Message = $"Invalid membershipId {id}" };
            }

            var user = await _userRepo.FindByIdAsync(userId);

            if (user == null)
            {
                return new MembershipServiceResponse { Success = false, Message = $"Invalid userId {userId}" };
            }

            if (user.MembershipId == id)
            {
                return new MembershipServiceResponse { Success = false, Message = $"The user has already signup for this membership." };
            }

            user.MembershipId = id;

            try
            {
                await _userRepo.UpdateAsync(user);
            }
            catch (Exception ex)
            {
                return new MembershipServiceResponse { Success = false, Message = "There has been an error updating user." };
            }

            membership.Users = [user];

            return new MembershipServiceResponse { Success = true, Membership = membership };
        }

        public async Task<MembershipServiceResponse> CancelMembership(int userId)
        {
            if (userId == null || userId <= 0)
            {
                return new MembershipServiceResponse { Success = false, Message = "UserId must be provided" };
            }

            var user = await _userRepo.FindByIdAsync(userId);

            if (user == null)
            {
                return new MembershipServiceResponse { Success = false, Message = $"No user with id {userId}" };
            }

            if (user.MembershipId == 1)
            {
                return new MembershipServiceResponse { Success = false, Message = "This user membership cannot be changed." };
            }

            user.MembershipId = 2;

            try
            {
                await _userRepo.UpdateAsync(user);
            }
            catch (Exception ex)
            {
                return new MembershipServiceResponse { Success = false, Message = "There has been an error updating the user membership." };
            }

            return new MembershipServiceResponse { Success = true };
        }

        public async Task<GetMembershipResponse> GetMembershipProgress(int userId)
        {
            var user = await _userRepo.FindByIdAsync(userId);

            if (user == null)
            {
                return new GetMembershipResponse { Success = false, Message = "Unable to find the user." };
            }

            var memberships = await _memberRepo.GetAllAsync();

            var orderedMemberships = memberships.OrderBy(m => m.PointsRequirement).ToList();

            var currentMembership = orderedMemberships.FirstOrDefault(m => m.Id == user.MembershipId);

            if (currentMembership == null)
            {
                return new GetMembershipResponse { Success = false, Message = "Unable to find current membership." };
            }

            var nextMembership = orderedMemberships.FirstOrDefault(m => m.PointsRequirement > currentMembership.PointsRequirement);
            var previousMembership = orderedMemberships.LastOrDefault(m => m.PointsRequirement < currentMembership.PointsRequirement);

            return new GetMembershipResponse
            {
                Success = true,
                Message = nextMembership != null ? "Next membership available" : "No futher membership available.",
                Point = user.LoyaltyPoints,
                CurrentMembership = currentMembership,
                NextMembership = nextMembership,
                PreviousMembership = previousMembership
            };
        }
    }
}
