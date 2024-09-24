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
        Task<User> GetUserByIdAsync(int id);
    }
}
