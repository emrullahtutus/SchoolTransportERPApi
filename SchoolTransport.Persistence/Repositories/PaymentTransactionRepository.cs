using Microsoft.EntityFrameworkCore;
using SchoolTransport.Application.DTOs.PaymentTransaction;
using SchoolTransport.Application.Repositories;
using SchoolTransport.Domain.Entities.Payment;
using SchoolTransport.Domain.Entities.Student;
using SchoolTransport.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTransport.Persistence.Repositories
{
    public class PaymentTransactionRepository : RepositoryBase<PaymentTransaction>, IPaymentTransactionRepository
    {
        public PaymentTransactionRepository(SchoolTransportDbContext context) : base(context)
        {
        }

        public async Task<List<PaymentTransaction>> GetTransactionByIdAsync(int studentId, string tenantId)
        {
            var transaction = await _context.PaymentTransactions
                .Where(p => p.StudentId == studentId && p.TenantId == tenantId)
                .ToListAsync();
            return transaction;
        }
    }

}
