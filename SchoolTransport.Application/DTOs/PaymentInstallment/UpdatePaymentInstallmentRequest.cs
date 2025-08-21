using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTransport.Application.DTOs.PaymentInstallment
{
    public class UpdatePaymentInstallmentRequest
    {
        public int Id { get; set; }
        public DateTime DueDate { get; set; }
        public decimal InstallmentAmount { get; set; }
        public bool IsPaid { get; set; }
        public DateTime? PaidDate { get; set; }
    
    }
}
