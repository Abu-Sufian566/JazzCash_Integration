using JazzCash_Integration.Input;
using JazzCash_Integration.Service;
using Microsoft.AspNetCore.Mvc;

namespace JazzCash_Integration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        [HttpPost("process")]
        public async Task<IActionResult> ProcessPayment([FromBody] PaymentRequestModel request)
        {
            var result = await _paymentService.MakePayment(request);
            return Ok(result);
        }
    }
}
