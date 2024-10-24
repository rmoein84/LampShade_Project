using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;
namespace _0_Framework.Application.ZarinPal
{
    public class ZarinPalFactory : IZarinPalFactory
    {
        private readonly IConfiguration _configuration;

        public string Prefix { get; set; }
        private string MerchantId { get; }

        public ZarinPalFactory(IConfiguration configuration)
        {
            _configuration = configuration;
            Prefix = _configuration.GetSection("payment")["method"];
            MerchantId = _configuration.GetSection("payment")["merchant"];
        }

        public PaymentResponse CreatePaymentRequest(string amount, string mobile, string email, string description,
             long orderId)
        {
            amount = amount.Replace(",", "");
            var finalAmount = int.Parse(amount);
            var siteUrl = _configuration.GetSection("payment")["siteUrl"];

            //var client = new RestClient($"https://{Prefix}.zarinpal.com/pg/v4/payment/request.json");
            var client = new RestClient($"https://sandbox.zarinpal.com/pg/v4/payment/request.json");
            var request = new RestRequest();
            request.Method = Method.Post;
            request.AddHeader("Content-Type", "application/json");
            //var body = new PaymentRequest
            //{
            //    Mobile = mobile,
            //    CallbackURL = $"{siteUrl}/Checkout?handler=CallBack&oId={orderId}",
            //    Description = description,
            //    Email = email,
            //    Amount = finalAmount,
            //    MerchantID = MerchantId
            //};
            var body = new
            {
                callback_url = $"{siteUrl}/Checkout?handler=CallBack&oId={orderId}",
                description = "توضیحات",
                amount = finalAmount,
                merchant_id = MerchantId,
                currency = "IRT"
            };
            request.AddJsonBody(body);
            var response = client.Execute(request);
            var result = JsonConvert.DeserializeObject<DataResponse>(response.Content);
            
            return result.Data;
            //return jsonSerializer.Deserialize<PaymentResponse>(response);
        }

        public VerificationResponse CreateVerificationRequest(string authorityId, string amount)
        {
            var client = new RestClient($"https://{Prefix}.zarinpal.com/pg/v4/payment/verify.json");

            var request = new RestRequest();
            request.Method = Method.Post;
            request.AddHeader("Content-Type", "application/json");

            amount = amount.Replace(",", "");
            var finalAmount = int.Parse(amount);

            //request.AddJsonBody(new VerificationRequest
            //{
            //    Amount = finalAmount,
            //    MerchantID = MerchantId,
            //    Authority = authority
            //});
            request.AddJsonBody(new
            {
                amount = finalAmount,
                merchant_id = MerchantId,
                authority = authorityId
            });
            var response = client.Execute(request);
            var result = JsonConvert.DeserializeObject<DataVerifyResponse>(response.Content);

            return result.Data;

            //var jsonSerializer = new JsonSerializer();
            //return jsonSerializer.Deserialize<VerificationResponse>(response);
        }
    }
}