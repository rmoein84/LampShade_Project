namespace _0_Framework.Application.ZarinPal
{
    public class VerificationResponse
    {
        public int Code { get; set; }
        public long Ref_id { get; set; }
    }
    public class VerifyDataResponse
    {
        public VerificationResponse Data { get; set; }

    }
}
