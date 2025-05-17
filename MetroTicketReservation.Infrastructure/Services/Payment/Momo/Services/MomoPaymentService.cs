using MetroTicketReservation.Application.Common.Interfaces.Services;
using MetroTicketReservation.Application.Common.Models;
using MetroTicketReservation.Application.Common.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using System.Security.Cryptography;
using System.Text;

namespace MetroTicketReservation.Infrastructure.Services.Payment.Momo.Services
{
    public class MomoPaymentService : IMomoPaymentService
    {
        private readonly MomoOptions _options;

        public MomoPaymentService(IOptions<MomoOptions> options)
        {
            _options = options.Value;
        }
        public async Task<MomoCreatePaymentResult> CreatePaymentAsync(MomoPaymentRequest model)
        {
            var rawData =
                $"partnerCode={_options.PartnerCode}" +
                $"&accessKey={_options.AccessKey}" +
                $"&requestId={model.OrderId}" +
                $"&amount={model.Amount}" +
                $"&orderId={model.OrderId}" +
                $"&orderInfo={model.OrderInfo}" +
                $"&returnUrl={_options.ReturnUrl}" +
                $"&notifyUrl={_options.NotifyUrl}" +
                $"&extraData=";

            var signature = ComputeHmacSha256(rawData, _options.SecretKey);

            var client = new RestClient(_options.MomoApiUrl);
            var request = new RestRequest() { Method = Method.Post };
            request.AddHeader("Content-Type", "application/json; charset=UTF-8");

            // Create an object representing the request data
            var requestData = new
            {
                accessKey = _options.AccessKey,
                partnerCode = _options.PartnerCode,
                requestType = _options.RequestType,
                notifyUrl = _options.NotifyUrl,
                returnUrl = _options.ReturnUrl,
                orderId = model.OrderId,
                amount = model.Amount.ToString(),
                orderInfo = model.OrderInfo,
                requestId = model.OrderId,
                extraData = "",
                signature
            };

            request.AddParameter("application/json", JsonConvert.SerializeObject(requestData), ParameterType.RequestBody);

            var response = await client.ExecuteAsync(request);
            var momoResponse = JsonConvert.DeserializeObject<MomoCreatePaymentResult>(response.Content);

            return momoResponse;
        }
        public MomoPaymentExecutionResult ProcessPaymentCallback(IQueryCollection collection)
        {
            var amount = collection.First(s => s.Key == "amount").Value;
            var orderInfo = collection.First(s => s.Key == "orderInfo").Value;
            var orderId = collection.First(s => s.Key == "orderId").Value;
            // chưa xác thực sinh trắc học nên dùng tạm errorCode
            var responseCode = collection.First(s => s.Key == "errorCode").Value;
            var message = collection.First(s => s.Key == "message").Value;

            return new MomoPaymentExecutionResult()
            {
                Amount = amount,
                OrderId = orderId,
                OrderInfo = orderInfo,
                ResponseCode = int.Parse(responseCode),
                Message = message,
            };
        }

        private string ComputeHmacSha256(string rawData, string secretKey)
        {
            var keyBytes = Encoding.UTF8.GetBytes(secretKey);
            var messageBytes = Encoding.UTF8.GetBytes(rawData);
            byte[] hashBytes;

            using (var hmac = new HMACSHA256(keyBytes))
            {
                hashBytes = hmac.ComputeHash(messageBytes);
            }

            var hashString = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

            return hashString;
        }
    }
}
