using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models;
using Models.DTOs;
using Repositories.Interface;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Services.Implement
{
    public class AuthService : IAuthService
    {
        private readonly IRepositoryBase<User> _userRepo;
        private readonly IConfiguration _config;

        public AuthService(IRepositoryBase<User> userRepo, IConfiguration config)
        {
            _userRepo = userRepo;
            _config = config;
        }

        public async Task<string> Login(string email, string password)
        {
            var user = await ValidateUser(email, password);

            if (user != null)
            {
                // Generate JWT token
                var tokenHandler = new JwtSecurityTokenHandler();
                IConfiguration config = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json", true, true)
                  .Build();
                var strConn = config["ConnectionStrings:PodBookingSystemDB"];
                var key = Encoding.UTF8.GetBytes(config["Jwt:Key"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.Role, user.RoleId.ToString())
                }),
                    Expires = DateTime.UtcNow.AddMinutes(15),
                    Issuer = config["Jwt:Issuer"],
                    Audience = config["Jwt:Audience"],
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }

            return null;
        }

        private async Task<User> ValidateUser(string email, string password)
        {
            var users = await _userRepo.GetAllAsync();
            if (!users.IsNullOrEmpty())
            {
                var user = users.FirstOrDefault(x => x.Email.Equals(email));
                if (user != null)
                {
                    if (user.Status == 1)
                    {
                        if (BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                        {
                            return user;
                        }
                    }
                }
            }
            return null;
        }

        public async Task<User> RegisterCustomer(CreateUserDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                throw new ArgumentException("Name cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(dto.Email))
            {
                throw new ArgumentException("Email cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(dto.Password))
            {
                throw new ArgumentException("Password cannot be empty.");
            }

            if (!new EmailAddressAttribute().IsValid(dto.Email))
            {
                throw new ArgumentException("Invalid email format.");
            }

            var users = await _userRepo.GetAllAsync();
            var existingUser = users.FirstOrDefault(u => u.Email.Equals(dto.Email, StringComparison.OrdinalIgnoreCase));
            if (existingUser != null)
            {
                throw new ArgumentException("Email is already in use.");
            }

            if (!IsPasswordValid(dto.Password))
            {
                throw new ArgumentException("Password must be at least 8 characters long, contain at least 1 capital letter, 1 normal letter, 1 special character, and 1 number.");
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var newUser = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = dto.Password,
                PasswordHash = passwordHash,
                Status = 0,
                MembershipId = 1,
                RoleId = 1,
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
    }
}
