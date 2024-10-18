﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class CreatePaymentResponse
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public MomoPaymentResponse MomoPaymentResponse { get; set; }
    }
}
