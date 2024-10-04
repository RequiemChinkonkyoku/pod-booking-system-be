using Microsoft.AspNetCore.Http;
using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IMomoService
    {
        public Task<CreatePaymentResponse> CreatePaymentAsync(CreatePaymentRequest model);
        public MomoExecuteResponse PaymentExecute(IQueryCollection collection);
    }
}
