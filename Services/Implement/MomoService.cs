using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Options;
using Models;
using Models.DTOs;
using Newtonsoft.Json;
using Repositories.Interface;
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
        private readonly IRepositoryBase<Booking> _bookingRepo;
        private readonly IRepositoryBase<Transaction> _transRepo;

        public MomoService(IOptions<MomoOptionModel> options, IRepositoryBase<Booking> bookingRepo, IRepositoryBase<Transaction> transRepo)
        {
            _options = options;
            _client = new HttpClient();
            _client.BaseAddress = new Uri(_options.Value.MomoApiUrl);
            _bookingRepo = bookingRepo;
            _transRepo = transRepo;
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

        public async Task<MomoExecuteResponse> PaymentExecute(IQueryCollection collection)
        {
            var amount = collection.First(s => s.Key == "amount").Value;
            var orderInfo = collection.First(s => s.Key == "orderInfo").Value;
            var orderId = collection.First(s => s.Key == "orderId").Value;
            var transId = collection.First(s => s.Key == "transId").Value;
            var errorCode = collection.First(s => s.Key == "errorCode").Value;
            var localMessage = collection.First(s => s.Key == "localMessage").Value;
            var bookingId = orderInfo.ToString().Split("BookingID: ")[1];

            if (errorCode.Equals("0"))
            {
                var booking = await _bookingRepo.FindByIdAsync(Int32.Parse(bookingId));

                if (booking == null)
                {
                    return new MomoExecuteResponse { Success = false, Message = "Unable to find booking with id " + bookingId };
                }

                booking.BookingStatusId = 3;

                try
                {
                    await _bookingRepo.UpdateAsync(booking);
                }
                catch (Exception ex)
                {
                    return new MomoExecuteResponse { Success = false, Message = "Unable to update booking status" };
                }
            }

            var trans = new Transaction
            {
                PaymentTime = DateTime.UtcNow,
                TotalPrice = Int32.Parse(amount),
                MethodId = 1,
                BookingId = Int32.Parse(bookingId),
                OrderId = orderId,
                PaymentId = transId,
                Status = (errorCode.Equals("0") ? 1 : 0)
            };

            try
            {
                await _transRepo.AddAsync(trans);
            }
            catch (Exception e)
            {
                return new MomoExecuteResponse { Success = false, Message = "There has been an error creating transaction." };
            }

            var response = new MomoExecuteResponse
            {
                Success = (errorCode.Equals("0") ? true : false),
                Message = localMessage,
                ErrorCode = errorCode,
                BookingId = Int32.Parse(bookingId),
                OrderId = orderId,
                Amount = Int32.Parse(amount),
                OrderInfo = orderInfo,
            };

            return response;
        }
    }
}
