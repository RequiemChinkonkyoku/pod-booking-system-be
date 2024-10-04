using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Options;
using Models.DTOs;
using Newtonsoft.Json;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implement
{
    public class MomoService : IMomoService
    {
        private readonly IOptions<MomoOptionModel> _options;
        private readonly HttpClient _client;

        public MomoService(IOptions<MomoOptionModel> options)
        {
            _options = options;
            _client = new HttpClient();
            _client.BaseAddress = new Uri(_options.Value.MomoApiUrl);
        }

        public async Task<CreatePaymentResponse> CreatePaymentAsync(CreatePaymentRequest request)
        {
            var bookingId = request.BookingId;
            request.OrderId = DateTime.UtcNow.Ticks.ToString();
            request.OrderInfo = "Customer: " + request.FullName + ". BookingID: " + bookingId;
            var rawData =
            $"partnerCode={_options.Value.PartnerCode}&accessKey={_options.Value.AccessKey}&requestId={request.OrderId}&amount={request.Amount}&orderId={request.OrderId}&orderInfo={request.OrderInfo}&returnUrl={_options.Value.ReturnUrl}&notifyUrl={_options.Value.NotifyUrl}&extraData=";
            var signature = ComputeHmacSha256(rawData, _options.Value.SecretKey);
            var requestData = new
            {
                accessKey = _options.Value.AccessKey,
                partnerCode = _options.Value.PartnerCode,
                requestType = "captureMoMoWallet",
                notifyUrl = _options.Value.NotifyUrl,
                returnUrl = _options.Value.ReturnUrl,
                orderId = request.OrderId,
                amount = request.Amount.ToString(),
                orderInfo = request.OrderInfo,
                requestId = request.OrderId,
                extraData = "",
                signature = signature
            };

            var content = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("", content);

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                return new CreatePaymentResponse { Success = false, Message = "Unable to execute payment request" };
            }

            var responseContent = await response.Content.ReadAsStringAsync();

            var momoResponse = JsonConvert.DeserializeObject<MomoPaymentResponse>(responseContent);

            return new CreatePaymentResponse { Success = true, MomoPaymentResponse = momoResponse };
        }

        private string ComputeHmacSha256(string message, string secretKey)
        {
            var keyBytes = Encoding.UTF8.GetBytes(secretKey);
            var messageBytes = Encoding.UTF8.GetBytes(message);

            byte[] hashBytes;

            using (var hmac = new HMACSHA256(keyBytes))
            {
                hashBytes = hmac.ComputeHash(messageBytes);
            }

            var hashString = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

            return hashString;
        }

        public MomoExecuteResponse PaymentExecute(IQueryCollection collection)
        {
            var amount = collection.First(s => s.Key == "amount").Value;
            var orderInfo = collection.First(s => s.Key == "orderInfo").Value;
            var orderId = collection.First(s => s.Key == "orderId").Value;
            var errorCode = collection.First(s => s.Key == "errorCode").Value;

            return new MomoExecuteResponse()
            {
                Amount = amount,
                OrderId = orderId,
                OrderInfo = orderInfo,
                ErrorCode = errorCode
            };
        }
    }
}
