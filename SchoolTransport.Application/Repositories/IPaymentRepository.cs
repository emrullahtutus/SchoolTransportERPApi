using SchoolTransport.Application.DTOs.Payment;
using SchoolTransport.Application.DTOs.School;
using SchoolTransport.Domain.Entities.Payment;
using SchoolTransport.Domain.Entities.School;
using SchoolTransport.Domain.Entities.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTransport.Application.Repositories
{
    public interface IPaymentRepository : IRepositoryBase<Payment>
    {
        Task<Payment> CreatePay(int studentId, string tenantId, CreatePaymentRequest paymentRequest);
        Task<List<Student>> GetUnpaidStudentsAsync(int schoolId, int vehicleId, int periodNumber, string tenantId);
        Task<Payment> GetStudentDebtSituation(int studentId, int periodNumber, string tenantId);
    }
}