using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTransport.Application.DTOs.PaymentInstallment
{
    public class PaymentInstallmentResponse
    {
        public int Id { get; set; }
        public int PaymentId { get; set; }
        public int MonthNumber { get; set; }
        public string MonthName { get; set; }
        public DateTime DueDate { get; set; }
        public decimal InstallmentAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal RemainingAmount { get; set; }
        public bool IsPaid { get; set; }
        public DateTime? PaidDate { get; set; }
    }
}
