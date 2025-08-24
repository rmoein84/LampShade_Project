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
        private string _baseUrl;
        private readonly IConfiguration _configuration;

        public string Prefix { get; set; }
        private string MerchantId { get; }

        public ZarinPalFactory(IConfiguration configuration)
        {
            _configuration = configuration;
            Prefix = _configuration.GetSection("payment")["method"];
            MerchantId = _configuration.GetSection("payment")["merchant"];
            _baseUrl = $"https://{Prefix}.zarinpal.com/pg/v4/payment/";
        }

        public PaymentResponse CreatePaymentRequest(string amount, string mobile, string email, string description,
             long orderId)
        {
            amount = amount.Replace(",", "");
            var finalAmount = int.Parse(amount);
            var siteUrl = _configuration.GetSection("payment")["siteUrl"];
            var client = new RestClient(_baseUrl);
            var request = new RestRequest("request.json", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            var body = new PaymentRequest
            {
                Mobile = mobile,
                CallbackURL = $"{siteUrl}/Checkout?handler=CallBack&oId={orderId}",
                Description = description,
                Email = email,
                Amount = finalAmount,
                MerchantID = MerchantId,
                Currency = "IRT"
            };
            request.AddJsonBody(body);
            var response = client.Execute(request);
            var result = JsonConvert.DeserializeObject<PaymentDataResponse>(response.Content);

            return result.Data;
        }

        public VerificationResponse CreateVerificationRequest(string authority, string amount)
        {
            var client = new RestClient(_baseUrl);

            var request = new RestRequest("verify.json", Method.Post);
            request.AddHeader("Content-Type", "application/json");

            amount = amount.Replace(",", "");
            var finalAmount = int.Parse(amount);

            request.AddJsonBody(new VerificationRequest
            {
                Amount = finalAmount,
                MerchantID = MerchantId,
                Authority = authority
            });

            var response = client.Execute(request);
            var result = JsonConvert.DeserializeObject<VerifyDataResponse>(response.Content);
            return result.Data;
        }
    }
}