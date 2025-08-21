using Microsoft.EntityFrameworkCore;
using SchoolTransport.Application.DTOs.Payment;
using SchoolTransport.Application.DTOs.School;
using SchoolTransport.Application.Repositories;
using SchoolTransport.Domain.Entities.Payment;
using SchoolTransport.Domain.Entities.School;
using SchoolTransport.Domain.Entities.Student;
using SchoolTransport.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTransport.Persistence.Repositories
{
    public class PaymentRepository : RepositoryBase<Payment>, IPaymentRepository
    {
        private readonly IPaymentTransactionRepository _paymentTransactionRepository;

        public PaymentRepository(SchoolTransportDbContext context, IPaymentTransactionRepository paymentTransactionRepository) : base(context)
        {
            _paymentTransactionRepository = paymentTransactionRepository;
        }

        public async Task<Payment> CreatePay(int studentId, string tenantId, CreatePaymentRequest paymentRequest)
        {
            var payment = await _context.Payments
                .Where(p => p.StudentId == studentId && p.TenantId == tenantId)
                .FirstOrDefaultAsync();
            return payment;
        }

        public async Task<Payment> GetStudentDebtSituation(int studentId, int periodNumber, string tenantId)
        {
            var payment = await _context.Payments
                .SingleOrDefaultAsync(s => s.StudentId == studentId && s.TenantId == tenantId);
            return payment;
        }

        public async Task<List<Student>> GetUnpaidStudentsAsync(int schoolId, int vehicleId, int periodNumber, string tenantId)
        {
            var today = DateTime.Now;
            var students = await _context.Students
                .Include(s => s.School)
                .Include(s => s.Payment)
                .Where(s => s.SchoolId == schoolId && s.VehicleId == vehicleId && s.TenantId == tenantId)
                .ToListAsync();

            var unpaidStudents = students
                .Where(s => s.Payment.DebtAmount < 0)
                .ToList();

            return unpaidStudents;
        }
    }
    }
