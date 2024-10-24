using IPE.SmsIrClient;
using Microsoft.Extensions.Configuration;

namespace _0_Framework.Application.Sms
{
    public class SmsService : ISmsService
    {
        private readonly IConfiguration _configuration;

        public SmsService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async void Send(string number, string message)
        {
            SmsIr smsIr = new SmsIr(GetToken());

            var bulkSendResult = await smsIr.BulkSendAsync(30007732002580,
                message,
                new string[] { number });
        }

        private string GetToken()
        {
            var smsSecrets = _configuration.GetSection("SmsSecrets");
            return smsSecrets["ApiKey"];
        }
    }
}