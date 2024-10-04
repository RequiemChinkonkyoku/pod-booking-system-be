using Microsoft.AspNetCore.Http;
using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IVnPayService
    {
        public string CreatePaymentUrl(CreatePaymentRequest request, HttpContext context);
    }
}
