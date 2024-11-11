using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models;
using Models.DTOs;
using Repositories.Interface;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Services.Implement
{
    public class UserService : IUserService
    {
        private readonly IRepositoryBase<User> _userRepo;
        private readonly IRepositoryBase<Booking> _bookingRepo;
        private readonly IConfiguration _config;
        private readonly IBookingService _bookingService;

        public UserService(IRepositoryBase<User> userRepo, IRepositoryBase<Booking> bookingRepo, IConfiguration config, IBookingService bookingService)
        {
            _userRepo = userRepo;
            _bookingRepo = bookingRepo;
            _config = config;
            _bookingService = bookingService;
        }

        public async Task<string> Login(string email, string password)
        {
            var users = await _userRepo.GetAllAsync();

            if (!users.IsNullOrEmpty())
            {
                var user = users.FirstOrDefault(x => x.Email.Equals(email));
                if (user != null)
                {
                    return user.Email;
                }
            }
            return null;
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _userRepo.GetAllAsync();
        }

        public async Task<User> CreateUserAsync(CreateUserDto dto)
        {
            // Validate Name
            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                throw new ArgumentException("Name cannot be empty.");
            }

            // Validate Email
            if (string.IsNullOrWhiteSpace(dto.Email))
            {
                throw new ArgumentException("Email cannot be empty.");
            }

            if (!new EmailAddressAttribute().IsValid(dto.Email))
            {
                throw new ArgumentException("Invalid email format.");
            }

            // Validate Password
            if (string.IsNullOrWhiteSpace(dto.Password))
            {
                throw new ArgumentException("Password cannot be empty.");
            }

            if (!IsPasswordValid(dto.Password))
            {
                throw new ArgumentException("Password must be at least 8 characters long, contain at least 1 capital letter, 1 normal letter, 1 special character, and 1 number.");
            }

            // Check if RoleId is within the valid range (1 to 4)
            if (dto.RoleId < 1 || dto.RoleId > 4)
            {
                throw new ArgumentException("RoleId must be between 1 and 4.");
            }

            // Check if Email already exists
            var users = await _userRepo.GetAllAsync();
            var existingUser = users.FirstOrDefault(u => u.Email.Equals(dto.Email, StringComparison.OrdinalIgnoreCase));
            if (existingUser != null)
            {
                throw new ArgumentException("Email is already in use.");
            }

            // Hash the password before storing it
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var newUser = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = dto.Password,
                PasswordHash = passwordHash,
                Status = 0,
                MembershipId = 1,
                RoleId = dto.RoleId,
            };

            await _userRepo.AddAsync(newUser);
            return newUser;
        }

        public async Task<User> CreateStaffAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentNullException("email");
            }

            if (!new EmailAddressAttribute().IsValid(email))
            {
                throw new ArgumentException("Invalid email format.");
            }
            var users = await _userRepo.GetAllAsync();
            var existingUser = users.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
            if (existingUser != null)
            {
                throw new ArgumentException("Email is already in use.");
            }

            var newUser = new User
            {
                Name = "STAFF",
                Email = email,
                Password = "Password@123456",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Password@123456"),
                Status = 1,
                MembershipId = 1,
                RoleId = 2,
            };

            await _userRepo.AddAsync(newUser);
            return newUser;
        }

        private bool IsPasswordValid(string password)
        {
            // [PASSWORD] At least 8 characters, 1 uppercase letter, 1 lowercase letter, 1 digit, and 1 special character
            var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");
            return regex.IsMatch(password);
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            var user = await _userRepo.FindByIdAsync(id);
            return user;
        }

        public async Task<User> UpdateUserAsync(int id, UpdateUserDto dto)
        {
            var existingUser = await GetUserByIdAsync(id);
            if (existingUser == null)
            {
                throw new ArgumentException("User not found.");
            }

            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                throw new ArgumentException("Name cannot be empty.");
            }

            if (!string.IsNullOrWhiteSpace(dto.Password))
            {
                if (!IsPasswordValid(dto.Password))
                {
                    throw new ArgumentException("Password must be at least 8 characters long, contain at least 1 capital letter, 1 normal letter, 1 special character, and 1 number.");
                }

                existingUser.Password = dto.Password;
            }

            existingUser.Name = dto.Name;

            await _userRepo.UpdateAsync(existingUser);

            return existingUser;
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            var user = await GetUserByIdAsync(userId);
            if (user == null)
            {
                throw new ArgumentException("User not found.");
            }

            var userBookings = _bookingRepo.GetAllAsync().Result.Where(b => b.UserId == userId);
            if (userBookings.Any())
            {
                var userValidBookings = userBookings.Where(b => (b.BookingStatusId == 2 || b.BookingStatusId == 3));
                if (userValidBookings.Any())
                {
                    foreach (var booking in userValidBookings)
                    {
                        try
                        {
                            await _bookingService.CancelBooking(booking.Id, userId);
                        }
                        catch (Exception ex)
                        {
                            throw new ArgumentException("Booking cancel error sth idk.");
                        }
                    }
                }
            }

            user.Status = -1;

            await _userRepo.UpdateAsync(user);
            return true;
        }

        public async Task<bool> UserUpdateNameAsync(int id, string name)
        {
            bool result = false;
            var user = await GetUserByIdAsync(id);
            if (user == null)
            {
                throw new ArgumentException("User not found.");
            }

            // Validate Name
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name cannot be empty.");
                //return result;
            }

            user.Name = name;
            await _userRepo.UpdateAsync(user);
            result = true;
            return result;
        }

        public async Task<bool> UserUpdatePassword(int id, string currentPassword, string newPassword)
        {
            bool result = false;
            var user = await GetUserByIdAsync(id);
            if (user == null)
            {
                throw new ArgumentException("User not found.");
            }
            if (user.Status != 1)
            {
                throw new ArgumentException("User is not active!");
            }
            if (!BCrypt.Net.BCrypt.Verify(currentPassword, user.PasswordHash))
            {
                throw new ArgumentException("Current password is not correct.");
            }

            // Validate Password
            if (string.IsNullOrWhiteSpace(currentPassword) || string.IsNullOrWhiteSpace(newPassword))
            {
                throw new ArgumentException("Password cannot be empty.");
            }
            if (BCrypt.Net.BCrypt.Verify(newPassword, user.PasswordHash))
            {
                throw new ArgumentException("Current password and new password must not be the same.");
            }
            if (!IsPasswordValid(newPassword))
            {
                throw new ArgumentException("Password must be at least 8 characters long, contain at least 1 capital letter, 1 normal letter, 1 special character, and 1 number.");
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
            user.Password = newPassword;
            user.PasswordHash = passwordHash;
            await _userRepo.UpdateAsync(user);
            result = true;
            return result;
        }
    }
}
