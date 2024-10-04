using Microsoft.AspNetCore.Http;
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

namespace Services.Implement
{
    public class VnPayService : IVnPayService
    {
        private readonly IOptions<VnPayOptionModel> _options;
        private readonly HttpClient _client;
        private readonly SortedList<string, string> _requestData = new SortedList<string, string>(new VnPayCompare());

        public VnPayService(IOptions<VnPayOptionModel> options)
        {
            _options = options;
            _client = new HttpClient();
            _client.BaseAddress = new Uri(_options.Value.BaseUrl);
        }

        public string CreatePaymentUrl(CreatePaymentRequest request, HttpContext context)
        {
            var timeZoneId = TimeZoneInfo.FindSystemTimeZoneById(_options.Value.TimeZoneId);
            var timeNow = TimeZoneInfo.ConvertTimeToUtc(DateTime.UtcNow, timeZoneId);
            var tick = DateTime.Now.Ticks.ToString();
            var urlCallback = _options.Value.ReturnUrl;

            var requestData = new
            {
                vnp_Version = _options.Value.Version,
                vnp_Command = _options.Value.Command,
                vnp_TmnCode = _options.Value.TmnCode,
                vnp_Amount = request.Amount,
                vnp_CreateDate = timeNow.ToString("yyyyMMddHHmmss"),
                vnp_CurrCode = _options.Value.CurrCode,
                vnp_IpAddr = GetIpAddress(context),
                vnp_Locale = _options.Value.Locale,
                vnp_OrderInfo = request.OrderInfo,
                vnp_OrderType = "Payment",
                vnp_ReturnUrl = _options.Value.ReturnUrl,
                vnp_TxnRef = tick
            };

            var paymentUrl = CreateRequestUrl(_options.Value.BaseUrl, _options.Value.HashSecret);

            return paymentUrl;
        }

        private string GetIpAddress(HttpContext context)
        {
            var ipAddress = string.Empty;
            try
            {
                var remoteIpAddress = context.Connection.RemoteIpAddress;

                if (remoteIpAddress != null)
                {
                    if (remoteIpAddress.AddressFamily == AddressFamily.InterNetworkV6)
                    {
                        remoteIpAddress = Dns.GetHostEntry(remoteIpAddress).AddressList
                            .FirstOrDefault(x => x.AddressFamily == AddressFamily.InterNetwork);
                    }

                    if (remoteIpAddress != null) ipAddress = remoteIpAddress.ToString();

                    return ipAddress;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return "127.0.0.1";
        }

        public string CreateRequestUrl(string baseUrl, string vnpHashSecret)
        {
            var data = new StringBuilder();

            foreach (var (key, value) in _requestData.Where(kv => !string.IsNullOrEmpty(kv.Value)))
            {
                data.Append(WebUtility.UrlEncode(key) + "=" + WebUtility.UrlEncode(value) + "&");
            }

            var querystring = data.ToString();

            baseUrl += "?" + querystring;

            var signData = querystring;

            if (signData.Length > 0)
            {
                signData = signData.Remove(data.Length - 1, 1);
            }

            var vnpSecureHash = HmacSha512(vnpHashSecret, signData);

            baseUrl += "vnp_SecureHash=" + vnpSecureHash;

            return baseUrl;
        }

        private string HmacSha512(string key, string inputData)
        {
            var hash = new StringBuilder();
            var keyBytes = Encoding.UTF8.GetBytes(key);
            var inputBytes = Encoding.UTF8.GetBytes(inputData);
            using (var hmac = new HMACSHA512(keyBytes))
            {
                var hashValue = hmac.ComputeHash(inputBytes);
                foreach (var theByte in hashValue)
                {
                    hash.Append(theByte.ToString("x2"));
                }
            }

            return hash.ToString();
        }
    }
}

public class VnPayCompare : IComparer<string>
{
    public int Compare(string x, string y)
    {
        if (x == y) return 0;
        if (x == null) return -1;
        if (y == null) return 1;
        var vnpCompare = CompareInfo.GetCompareInfo("en-US");
        return vnpCompare.Compare(x, y, CompareOptions.Ordinal);
    }
}
