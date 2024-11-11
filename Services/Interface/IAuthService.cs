using Models;
using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IAuthService
    {
        Task<string> Login (string email, string password);
        Task<User> RegisterCustomer(CreateUserDto dto);
        Task<bool> VerifyOtpAsync(string email, string otpCode);
        Task<string> ForgotPasswordAsync(string email);
        Task<bool> ResetPasswordAsync(string email, string otpCode, string newPassword);
    }
}
