using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolTransport.Application.Abstract;
using SchoolTransport.Application.DTOs.PaymentTransaction;

namespace SchoolTransport.API.Controllers
{
    [ApiController]
    [Route("api/payment-transactions")]
    [Authorize(AuthenticationSchemes = "Admin")]

    public class PaymentTransactionController : BaseController
    {
        private readonly IPaymentTransactionService _transactionService;

        public PaymentTransactionController(IPaymentTransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<PaymentTransactionResponse>>> GetTransactionById(int id)
        {
            var tenantId = GetTenantId();
            var transaction = await _transactionService.GetTransactionByIdAsync(id, tenantId);
            return Ok(transaction);
        }
    }
}
