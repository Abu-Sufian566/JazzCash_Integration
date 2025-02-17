namespace JazzCash_Integration.Input
{
    public class PaymentRequestModel
    {
        public string MerchantID { get; set; } = "MC147910";
        public string Password { get; set; } = "4v451w21ux";
        public string IntegritySalt { get; set; } = "42e1021b8y";
        public string ReturnURL { get; set; } = "https://www.asaanrishta.com/";
        public string IPNURL { get; set; } = "https://www.asaanrishta.com/";
        public string? Amount { get; set; }
        public string? OrderID { get; set; }
        public string? Currency { get; set; } = "PKR";
        public string? ExpiryTime { get; set; }
        public string Language { get; set; } = "EN";
        public string Version { get; set; } = "2.0";
    }
}
