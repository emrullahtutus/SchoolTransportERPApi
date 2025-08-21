using AutoMapper;
using SchoolTransport.Application.Abstract;
using SchoolTransport.Application.DTOs.PaymentTransaction;
using SchoolTransport.Application.Repositories;
using SchoolTransport.Domain.Entities.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolTransport.Application.Exceptions.NotFound; // Bu satırı ekledim

namespace SchoolTransport.Application.Concrete
{
    public class PaymentTransactionService : IPaymentTransactionService
    {
        private readonly IPaymentTransactionRepository _transactionRepository;
        private readonly IMapper _mapper;

        public PaymentTransactionService(IPaymentTransactionRepository transactionRepository, IMapper mapper)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
        }

        public async Task<List<PaymentTransactionResponse>> GetTransactionByIdAsync(int studentId, string tenantId)
        {
            List<PaymentTransaction> paymentTransactions = await _transactionRepository.GetTransactionByIdAsync(studentId, tenantId);

            if (paymentTransactions == null || !paymentTransactions.Any())
            {
                throw new PaymentTransactionNotFoundException($"Öğrenci ID'si {studentId} olan ödeme işlemi bulunamadı.");
            }

            List<PaymentTransactionResponse> responseList = new List<PaymentTransactionResponse>();
            foreach (var transaction in paymentTransactions)
            {
                var response = new PaymentTransactionResponse
                {
                    Amount = transaction.Amount,
                    PaymentDate = transaction.PaymentDate.ToString("dd.MM.yyyy HH:mm"),
                    Description = transaction.Description
                };

                responseList.Add(response);
            }
            return responseList;
        }
    }
}