using SchoolTransport.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTransport.Domain.Entities.Payment
{
    public class Payment : BaseEntity
    {
        public int StudentId { get; set; }
        public Student.Student Student { get; set; }

        // Ücret Bilgileri
        public decimal MonthlyFee { get; set; } 
        public decimal TotalFee { get; set; } 
        public decimal PaidAmount { get; set; } // Ödenen toplam tutar
        public decimal RemainingAmount { get; set; } // Kalan borç

        // Durum Bilgileri
        public DateTime? LastPaymentDate { get; set; }
        public string? Notes { get; set; }
        public decimal? DebtAmount { get; set; }

        // Navigation Properties
        public ICollection<PaymentTransaction> PaymentTransactions { get; set; } = new List<PaymentTransaction>();
      
    }
}
