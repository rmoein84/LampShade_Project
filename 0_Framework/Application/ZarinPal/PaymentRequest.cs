using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace _0_Framework.Application.ZarinPal
{
    public class PaymentRequest
    {
        [JsonPropertyName("mobile")]
        public string Mobile { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("callback_url")]
        public string CallbackURL { get; set; }

        [JsonPropertyName("merchant_id")]
        public string MerchantID { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public string Currency { get; set; }
    }
}
