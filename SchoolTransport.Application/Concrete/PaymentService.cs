using AutoMapper;
using SchoolTransport.Application.Abstract;
using SchoolTransport.Application.DTOs.Payment;
using SchoolTransport.Application.DTOs.PaymentTransaction;
using SchoolTransport.Application.DTOs.School;
using SchoolTransport.Application.DTOs.Student;
using SchoolTransport.Application.Exceptions.BadRequest;
using SchoolTransport.Application.Exceptions.NotFound;
using SchoolTransport.Application.Repositories;
using SchoolTransport.Domain.Entities.Payment;
using SchoolTransport.Domain.Entities.School;
using SchoolTransport.Domain.Entities.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTransport.Application.Concrete
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IPaymentTransactionRepository _transactionRepository;
        private readonly IMapper _mapper;
        private readonly ISchoolRepository _schoolRepository;
        private readonly ISchoolService _schoolService;
        private readonly IStudentRepository _studentRepository;

        public PaymentService(IPaymentRepository paymentRepository, IMapper mapper, ISchoolRepository schoolRepository, ISchoolService schoolService, IStudentRepository studentRepository)
        {
            _paymentRepository = paymentRepository;
            _mapper = mapper;
            _schoolRepository = schoolRepository;
            _schoolService = schoolService;
            _studentRepository = studentRepository;
        }

        public async Task<List<PaymentResponse>> GetAllPaymentsAsync(string tenantId)
        {
            var payments = await _paymentRepository.GetAllAsync(tenantId);
            return _mapper.Map<List<PaymentResponse>>(payments);
        }

        public async Task<PaymentResponse> GetPaymentByIdAsync(int id, string tenantId)
        {
            var payment = await _paymentRepository.GetByIdAsync(id, tenantId);
            if (payment == null)
            {
                throw new PaymentNotFoundException($"ID'si {id} olan ödeme bulunamadı.");
            }
            return _mapper.Map<PaymentResponse>(payment);
        }

        public async Task<PaymentResponse> CreatePaymentAsync(CreatePaymentRequest request, string tenantId)
        {
            if (request == null)
            {
                throw new PaymentBadRequestException("Ödeme oluşturma isteği boş olamaz.");
            }

            var payment = _mapper.Map<Payment>(request);
            payment.TenantId = tenantId;
            await _paymentRepository.AddAsync(payment);
            await _paymentRepository.SaveChangesAsync();
            return _mapper.Map<PaymentResponse>(payment);
        }

        public async Task<PaymentResponse> UpdatePaymentAsync(UpdatePaymentRequest request, string tenantId)
        {
            if (request == null)
            {
                throw new PaymentBadRequestException("Ödeme güncelleme isteği boş olamaz.");
            }

            var payment = await _paymentRepository.GetByIdAsync(request.Id, tenantId, tracking: true);
            if (payment == null)
            {
                throw new PaymentNotFoundException($"ID'si {request.Id} olan ödeme bulunamadı.");
            }

            _mapper.Map(request, payment);
            payment.TenantId = tenantId;
            await _paymentRepository.UpdateAsync(payment);
            await _paymentRepository.SaveChangesAsync();
            return _mapper.Map<PaymentResponse>(payment);
        }

        public async Task DeletePaymentAsync(int id, string tenantId)
        {
            var payment = await _paymentRepository.GetByIdAsync(id, tenantId);
            if (payment == null)
            {
                throw new PaymentNotFoundException($"ID'si {id} olan ödeme bulunamadı.");
            }

            await _paymentRepository.DeleteByIdAsync(id, tenantId);
            await _paymentRepository.SaveChangesAsync();
        }

        public async Task<PaymentResponse> CreatePay(int studentId, CreatePaymentRequest paymentRequest, string tenantId)
        {
            var payment = await _paymentRepository.CreatePay(studentId, tenantId, paymentRequest);

            if (payment == null)
            {
                throw new PaymentNotFoundException($"Öğrenci ID'si {studentId} olan ödeme planı bulunamadı.");
            }
            if (paymentRequest == null)
            {
                throw new PaymentBadRequestException("Ödeme isteği boş olamaz.");
            }

            payment.PaidAmount += paymentRequest.Pay;
            payment.RemainingAmount = payment.TotalFee - payment.PaidAmount;
            payment.LastPaymentDate = DateTime.UtcNow;
            payment.Notes = paymentRequest.Notes;

            var transaction = new PaymentTransaction
            {
                PaymentId = payment.Id,
                Amount = paymentRequest.Pay,
                PaymentDate = DateTime.UtcNow,
                Description = paymentRequest.Notes,
                StudentId = paymentRequest.StudentId,
                TenantId = tenantId
            };

            payment.PaymentTransactions.Add(transaction);

            await _paymentRepository.SaveChangesAsync();

            var response = new PaymentResponse
            {
                Id = payment.Id,
                StudentId = payment.StudentId,
                TotalFee = payment.TotalFee,
                PaidAmount = payment.PaidAmount,
                RemainingAmount = payment.RemainingAmount,
                LastPaymentDate = payment.LastPaymentDate,
                Notes = payment.Notes,
                DebtAmount = await GetStudentDebtSituation(studentId, paymentRequest.periodNumber, tenantId)
            };

            return response;
        }

        public async Task<List<StudentResponse>> GetUnpaidStudentsAsync(int schoolId, int vehicleId, int periodNumber, string tenantId)
        {
            var students = await _paymentRepository.GetUnpaidStudentsAsync(schoolId, vehicleId, periodNumber, tenantId);
            return _mapper.Map<List<StudentResponse>>(students);
        }

        public async Task<decimal> GetStudentDebtSituation(int studentId, int periodNumber, string tenantId)
        {
            var payment = await _paymentRepository.GetStudentDebtSituation(studentId, periodNumber, tenantId);
            if (payment == null)
            {
                throw new PaymentNotFoundException($"Öğrenci ID'si {studentId} olan ödeme durumu bulunamadı.");
            }

            var mustPay = payment.MonthlyFee * periodNumber;
            var debtAmount = payment.PaidAmount - mustPay;
            payment.DebtAmount = debtAmount;
            payment.TenantId = tenantId;
            await _paymentRepository.SaveChangesAsync();
            return debtAmount;
        }
    }
}