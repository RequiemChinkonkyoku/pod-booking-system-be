﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Models.DTOs;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Globalization;
using Microsoft.Extensions.Configuration;
using Repositories.Interface;
using Models;

namespace Services.Implement
{
    public class VnPayService : IVnPayService
    {
        private readonly IConfiguration _configuration;
        private readonly IRepositoryBase<Booking> _bookingRepo;
        private readonly IRepositoryBase<Transaction> _transactionRepo;
        private readonly IRepositoryBase<User> _userRepo;
        private readonly IMembershipService _memberService;

        public VnPayService(IConfiguration configuration,
                            IRepositoryBase<Booking> bookingRepo,
                            IRepositoryBase<Transaction> transactionRepo, 
                            IRepositoryBase<User> userRepo,
                            IMembershipService memberService)
        {
            _configuration = configuration;
            _bookingRepo = bookingRepo;
            _transactionRepo = transactionRepo;
            _userRepo = userRepo;
            _memberService = memberService;
        }

        public async Task<string> CreatePaymentUrl(VnpayInfoModel model, HttpContext context)
        {
            var user = await _userRepo.FindByIdAsync(model.UserId);
            var timeZoneById = TimeZoneInfo.FindSystemTimeZoneById(_configuration["Vnpay:TimeZoneId"]);
            var timeNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneById);
            var tick = DateTime.Now.Ticks.ToString();
            var pay = new VnPayLibrary();
            var urlCallBack = _configuration["Vnpay:ReturnUrl"];

            pay.AddRequestData("vnp_Version", _configuration["Vnpay:Version"]);
            pay.AddRequestData("vnp_Command", _configuration["Vnpay:Command"]);
            pay.AddRequestData("vnp_TmnCode", _configuration["Vnpay:TmnCode"]);
            pay.AddRequestData("vnp_Amount", ((int)model.Amount * 100).ToString());
            pay.AddRequestData("vnp_CreateDate", timeNow.ToString("yyyyMMddHHmmss"));
            pay.AddRequestData("vnp_CurrCode", _configuration["Vnpay:CurrCode"]);
            pay.AddRequestData("vnp_IpAddr", pay.GetIpAddress(context));
            pay.AddRequestData("vnp_Locale", _configuration["Vnpay:Locale"]);
            pay.AddRequestData("vnp_OrderInfo", $"Name: {user.Name}, Amount: {model.Amount}, BookingId: {model.BookingId} ");
            pay.AddRequestData("vnp_OrderType", model.OrderType);
            pay.AddRequestData("vnp_ReturnUrl", urlCallBack);
            pay.AddRequestData("vnp_TxnRef", tick);

            var paymentUrl =
                pay.CreateRequestUrl(_configuration["Vnpay:BaseUrl"], _configuration["Vnpay:HashSecret"]);

            return paymentUrl;
        }

        public async Task<VnpayResponseModel> PaymentExecute(IQueryCollection collections)
        {
            var pay = new VnPayLibrary();

            var response = pay.GetFullResponseData(collections, _configuration["Vnpay:HashSecret"]);

            if (!response.Success)
            {
                response.Message = $"Payment unsuccessful. Response code: {response.VnPayResponseCode}";

                return response;
            }

            if (response.VnPayResponseCode.Equals("24"))
            {
                response.Message = $"Payment cancelled. Response code: {response.VnPayResponseCode}";

                return response;
            }

            var orderDescriptionParts = response.OrderDescription.Split(", ");

            var bookingId = "";

            var amount = "";

            foreach (var part in orderDescriptionParts)
            {
                if (part.StartsWith("BookingId:"))
                {
                    bookingId = part.Replace("BookingId: ", "").Trim();
                }

                if (part.StartsWith("Amount:"))
                {
                    amount = part.Replace("Amount: ", "").Trim();
                }
            }

            response.BookingId = bookingId;
            response.Amount = amount;

            var booking = await _bookingRepo.FindByIdAsync(Int32.Parse(bookingId));

            if (booking.BookingStatusId != 2 && booking.BookingStatusId != 3)
            {
                response.Message = "Payment can only be done for Pending or Reserved bookings.";

                return response;
            }

            booking.BookingStatusId = 3;

            try
            {
                await _bookingRepo.UpdateAsync(booking);
            }
            catch (Exception ex)
            {
                response.Message = $"There has been an error updating booking status {ex.Message}";

                return response;
            }

            var transaction = new Transaction
            {
                OrderId = response.OrderId,
                PaymentId = response.PaymentId,
                PaymentTime = DateTime.UtcNow,
                TotalPrice = Int32.Parse(amount),
                Status = response.Success ? 1 : 0,
                MethodId = 2,
                BookingId = Int32.Parse(bookingId)
            };

            try
            {
                await _transactionRepo.AddAsync(transaction);
            }
            catch (Exception ex)
            {
                response.Message = $"There has been an error adding transaction. {ex.Message}";

                return response;
            }

            var user = await _userRepo.FindByIdAsync(booking.UserId);
            var pointGained = (int)Math.Floor(Int32.Parse(amount) / 1000.0);
            user.LoyaltyPoints += pointGained;

            while (true)
            {
                var membershipProgress = await _memberService.GetMembershipProgress(booking.UserId);
                var nextMembership = membershipProgress.NextMembership;

                if (nextMembership != null && user.LoyaltyPoints >= nextMembership.PointsRequirement)
                {
                    user.MembershipId = nextMembership.Id;
                }
                else
                {
                    break;
                }
            }

            try
            {
                await _userRepo.UpdateAsync(user);
            }
            catch (Exception ex)
            {
                return new VnpayResponseModel { Success = false, Message = "Unable to update user loyalty point." };
            }

            return response;
        }
    }
}
