using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTransport.Application.DTOs.PaymentInstallment
{
    public class CreatePaymentInstallmentRequest
    {
        public int PaymentId { get; set; }
        public int MonthNumber { get; set; }
        public string MonthName { get; set; }
        public DateTime DueDate { get; set; }
        public decimal InstallmentAmount { get; set; }

    }
}
