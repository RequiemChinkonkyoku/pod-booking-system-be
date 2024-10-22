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
        public Task<string> CreatePaymentUrl(VnpayInfoModel request, HttpContext context);

        public Task<VnpayResponseModel> PaymentExecute(IQueryCollection collections);
    }
}
