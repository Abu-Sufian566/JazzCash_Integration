using JazzCash_Integration.Input;
using Microsoft.OpenApi.Models;
using RestSharp;
using System.Security.Cryptography;
using System.Text;

namespace JazzCash_Integration.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly IConfiguration _configuration;

        public PaymentRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<string> ProcessPayment(PaymentRequestModel payment)
        {
            var merchantID = _configuration["JazzCash:MerchantID"];
            var password = _configuration["JazzCash:Password"];
            var integritySalt = _configuration["JazzCash:IntegritySalt"];
            var returnURL = _configuration["JazzCash:ReturnURL"];
            var ipnURL = _configuration["JazzCash:IPNURL"];
            var apiURL = _configuration["JazzCash:APIURL"];

            var txnRefNo = Guid.NewGuid().ToString("N").Substring(0, 10);
            var txnDateTime = DateTime.UtcNow.AddHours(5).ToString("yyyyMMddHHmmss");
            var expiryDateTime = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyyMMddHHmmss");

            var postData = $"amount={payment.Amount}&currency={payment.Currency}&merchant_id={merchantID}&password={password}&txn_ref_no={txnRefNo}&txn_date_time={txnDateTime}&expiry_date_time={expiryDateTime}&return_url={returnURL}&ipn_url={ipnURL}&version={payment.Version}&language={payment.Language}";

           var secureHash = ComputeHash(integritySalt, merchantID, password, txnRefNo, txnDateTime, expiryDateTime, payment.Amount, payment.Currency, returnURL, ipnURL, payment.Version, payment.Language);
            postData += $"&secure_hash={secureHash}";

            var client = new RestClient(apiURL);
            var request = new RestRequest("", Method.Post);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", postData, ParameterType.RequestBody);

            var response = await client.ExecuteAsync(request);
            return response.Content;
        }

        private string ComputeHash(string integritySalt, string merchantID, string password,  string txnRefNo, string txnDateTime, string expiryDateTime, string amount,string Currency, string returnURL, string ipnURL, string version, string language)
        {
            string dataToHash = $"{integritySalt}&{merchantID}&{password}&{amount}&{Currency}&{txnRefNo}&{txnDateTime}&{expiryDateTime}&{returnURL}&{ipnURL}&{version}&{language}";

            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(dataToHash);
                var hash = sha256.ComputeHash(bytes);
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }

    }
}
