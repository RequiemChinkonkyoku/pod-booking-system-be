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
using System.Net.Mail;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Services.Implement
{
    public class AuthService : IAuthService
    {
        private readonly IRepositoryBase<User> _userRepo;
        private readonly IRepositoryBase<UserOtp> _userOtpRepo;
        private readonly IConfiguration _config;

        public AuthService(IRepositoryBase<User> userRepo, IConfiguration config, IRepositoryBase<UserOtp> userOtpRepo)
        {
            _userRepo = userRepo;
            _config = config;
            _userOtpRepo = userOtpRepo;
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
                    Expires = DateTime.UtcNow.AddMinutes(90),
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
            await SendOtpAsync(dto.Email);

            return newUser;
        }

        private bool IsPasswordValid(string password)
        {
            // [PASSWORD] At least 8 characters, 1 uppercase letter, 1 lowercase letter, 1 digit, and 1 special character
            var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");
            return regex.IsMatch(password);
        }

        private string SendEmailAsync(string _to, string _subject, string name, string otpCode)
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            var _email = config["GmailSender:Email"];
            var _password = config["GmailSender:Password"];

            // Read the email template
            string templatePath = Path.Combine(Directory.GetCurrentDirectory(), "email_template.html");
            string emailBody = File.ReadAllText(templatePath);

            // Replace placeholders with actual values
            emailBody = emailBody.Replace("{{Name}}", name)
                                 .Replace("{{OTPCode}}", otpCode);

            MailMessage message = new MailMessage(_email, _to, _subject, emailBody)
            {
                BodyEncoding = System.Text.Encoding.UTF8,
                SubjectEncoding = System.Text.Encoding.UTF8,
                IsBodyHtml = true,
                ReplyToList = { new MailAddress(_email) },
                Sender = new MailAddress(_email)
            };

            using var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                EnableSsl = true,
                Credentials = new NetworkCredential(_email, _password)
            };

            try
            {
                smtpClient.Send(message);
                return "A verification email has been sent to your inbox.";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "Email cannot be sent.";
            }
        }


        private async Task<bool> SendOtpAsync(string email)
        {
            // Step 1: Retrieve the user based on the provided email.
            var users = await _userRepo.GetAllAsync();
            var user = users.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

            if (user == null)
            {
                throw new ArgumentException("No user found with this email.");
            }

            // Step 2: Check if the user is already verified.
            if (user.Status == 1)
            {
                throw new ArgumentException("User is already verified.");
            }

            // Step 3: Check if an existing OTP for the user is still valid.
            var otpList = await _userOtpRepo.GetAllAsync();
            var existingOtp = otpList.FirstOrDefault(o => o.UserId == user.Id);
            var currentTime = DateTime.UtcNow;

            if (existingOtp != null && existingOtp.ExpirationTime > currentTime)
            {
                // If the existing OTP is still valid, enforce a cooldown period before resending.
                var cooldownPeriod = TimeSpan.FromMinutes(2);
                if (existingOtp.ExpirationTime - currentTime < cooldownPeriod)
                {
                    throw new ArgumentException("OTP was recently generated. Please wait before requesting a new one.");
                }
            }

            // Step 4: Generate a new OTP with 3 letters and 4 numbers.
            string newOtpCode = await GenerateCustomOtp();
            var newExpirationTime = currentTime.AddMinutes(15); // Set expiration time for 15 minutes.

            var newUserOtp = new UserOtp
            {
                OtpCode = newOtpCode,
                ExpirationTime = newExpirationTime,
                UserId = user.Id
            };
            await _userOtpRepo.AddAsync(newUserOtp);

            // Step 5: Send the OTP via email.
            SendEmailAsync(user.Email, "noreply", user.Name, newOtpCode);

            return true;
        }

        private async Task<string> GenerateCustomOtp()
        {
            const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string numbers = "0123456789";
            var random = new Random();

            var letterPart = new string(Enumerable.Repeat(letters, 3)
                                                  .Select(s => s[random.Next(s.Length)])
                                                  .ToArray());

            var numberPart = new string(Enumerable.Repeat(numbers, 4)
                                                  .Select(s => s[random.Next(s.Length)])
                                                  .ToArray());

            string result = letterPart + numberPart;

            var otpList = await _userOtpRepo.GetAllAsync();
            var existingOtp = otpList.FirstOrDefault(o => o.OtpCode.Equals(result, StringComparison.OrdinalIgnoreCase));
            if (existingOtp != null)
            {
                result = await GenerateCustomOtp();
            }

            return result;
        }

        public async Task<bool> VerifyOtpAsync(string email, string otpCode)
        {
            // Step 1: Retrieve the user by their email.
            var users = await _userRepo.GetAllAsync();
            var user = users.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

            if (user == null)
            {
                throw new ArgumentException("No user found with this email.");
            }

            // Step 2: Retrieve the OTP record for the user.
            var otpList = await _userOtpRepo.GetAllAsync();
            var existingOtp = otpList.FirstOrDefault(o => o.UserId == user.Id);

            if (existingOtp == null)
            {
                throw new ArgumentException("No OTP record found for this user.");
            }

            // Step 3: Check if the provided OTP matches the stored OTP.
            if (!existingOtp.OtpCode.Equals(otpCode, StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException("Invalid OTP code.");
            }

            // Step 4: Check if the OTP has expired.
            if (existingOtp.ExpirationTime < DateTime.UtcNow)
            {
                throw new ArgumentException("The OTP has expired. Please request a new OTP.");
            }

            // Step 5: Check if the OTP has already been used.
            if (existingOtp.IsExpiredOrUsed)
            {
                throw new ArgumentException("This OTP code has already been used.");
            }

            // Step 6: Update user status to verified (Status = 1).
            user.Status = 1;
            await _userRepo.UpdateAsync(user);

            // Step 7: Mark the OTP as used to prevent reuse.
            existingOtp.IsExpiredOrUsed = true;
            await _userOtpRepo.UpdateAsync(existingOtp);

            // Step 8: Return true to indicate successful verification.
            return true;
        }

    }
}
