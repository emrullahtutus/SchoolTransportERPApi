using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTransport.Application.DTOs.Payment
{
    public class PaymentResponse
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public decimal TotalFee { get; set; }
  
        public decimal PaidAmount { get; set; }
        public decimal RemainingAmount { get; set; }
        public DateTime? LastPaymentDate { get; set; }
        public string Notes { get; set; }
        public decimal DebtAmount { get; set; }
    }
}
