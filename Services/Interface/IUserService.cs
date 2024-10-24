using Models;
using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IUserService
    {
        Task<string> Login(string email, string password);
        Task<List<User>> GetUsersAsync();
        Task<User> CreateUserAsync(CreateUserDto dto);
        Task<User> CreateStaffAsync(string email);
        Task<User> UpdateUserAsync(int id, UpdateUserDto dto);
        Task<bool> DeleteUserAsync(int id);
        Task<User> GetUserByIdAsync(int id);
        Task<bool> UserUpdateNameAsync(int id, string name);
        Task<bool> UserUpdatePassword(int id, string currentPassword, string newPassword);
    }
}
