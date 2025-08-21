using SchoolTransport.Application.DTOs.Payment;
using SchoolTransport.Application.DTOs.PaymentTransaction;
using SchoolTransport.Application.DTOs.School;
using SchoolTransport.Application.DTOs.Student;
using SchoolTransport.Domain.Entities.Payment;
using SchoolTransport.Domain.Entities.School;
using SchoolTransport.Domain.Entities.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTransport.Application.Abstract
{
    public interface IPaymentService
    {
        Task<List<PaymentResponse>> GetAllPaymentsAsync(string tenantId);
        Task<PaymentResponse> GetPaymentByIdAsync(int id, string tenantId);
        Task<PaymentResponse> CreatePaymentAsync(CreatePaymentRequest request, string tenantId);
        Task<PaymentResponse> UpdatePaymentAsync(UpdatePaymentRequest request, string tenantId);
        Task DeletePaymentAsync(int id, string tenantId);
        Task<PaymentResponse> CreatePay(int studentId, CreatePaymentRequest paymentRequest, string tenantId);
        Task<List<StudentResponse>> GetUnpaidStudentsAsync(int schoolId, int vehicleId, int periodNumber, string tenantId);
        Task<decimal> GetStudentDebtSituation(int studentId, int periodNumber, string tenantId);
    }
}
