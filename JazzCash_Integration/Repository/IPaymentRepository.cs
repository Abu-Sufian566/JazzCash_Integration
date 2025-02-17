using JazzCash_Integration.Input;

namespace JazzCash_Integration.Repository
{
    public interface IPaymentRepository
    {
        Task<string> ProcessPayment(PaymentRequestModel payment);
    }
}
