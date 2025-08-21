using SchoolTransport.Application.DTOs.PaymentTransaction;
using SchoolTransport.Domain.Entities.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTransport.Application.Abstract
{
    public interface IPaymentTransactionService
    {
        Task<List<PaymentTransactionResponse>> GetTransactionByIdAsync(int studentId, string tenantId);
    }
}
