using JazzCash_Integration.Input;

namespace JazzCash_Integration.Service
{
    public interface IPaymentService
    {
        Task<string> MakePayment(PaymentRequestModel request);
    }
}
