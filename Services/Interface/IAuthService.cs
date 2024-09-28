﻿using Models;
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
    }
}
