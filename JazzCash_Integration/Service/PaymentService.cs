using JazzCash_Integration.Input;
using JazzCash_Integration.Repository;

namespace JazzCash_Integration.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<string> MakePayment(PaymentRequestModel request)
        {
            return await _paymentRepository.ProcessPayment(request);
        }
    }
}
