using System.Text.Json.Serialization;

namespace _0_Framework.Application.ZarinPal
{
    public class VerificationRequest
    {
        public int Amount { get; set; }
        [JsonPropertyName("merchant_id")]
        public string MerchantID { get; set; }
        public string Authority { get; set; }
    }
}
