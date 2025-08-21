using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolTransport.Application.Abstract;
using SchoolTransport.Application.DTOs.Payment;

namespace SchoolTransport.API.Controllers
{
    [ApiController]
    [Route("api/payments")]
    [Authorize(AuthenticationSchemes = "Admin")]
   
    public class PaymentController : BaseController
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("{studentId}")]
        public async Task<IActionResult> CreatePayment(int studentId, [FromBody] CreatePaymentRequest request)
        {
            var tenantId = GetTenantId();
            var result = await _paymentService.CreatePay(studentId, request, tenantId);
            return Ok(result);
        }

        [HttpGet("unpaid-students/{schoolId}/{vehicleId}/{periodNumber}")]
        public async Task<IActionResult> GetUnpaidStudents(int schoolId, int vehicleId, int periodNumber)
        {
            var tenantId = GetTenantId();
            var result = await _paymentService.GetUnpaidStudentsAsync(schoolId, vehicleId, periodNumber, tenantId);
            return Ok(result);
        }

        [HttpGet("student/{studentId}/debt-situation")]
        public async Task<IActionResult> GetStudentDebtSituation(int studentId, int periodNumber)
        {
            var tenantId = GetTenantId();
            var debtAmount = await _paymentService.GetStudentDebtSituation(studentId, periodNumber, tenantId);
            return Ok(debtAmount);
        }
    }
}