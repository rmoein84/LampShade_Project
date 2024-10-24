namespace _0_Framework.Application.ZarinPal
{
    public class PaymentResponse
    {
        public int Code { get; set; }
        public string Authority { get; set; }
    }
    public class DataResponse
    {
        public PaymentResponse Data { get; set; }
    }
    public class DataVerifyResponse
    {
        public VerificationResponse Data { get; set; }

    }
}
