using SchoolTransport.Domain.Common;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTransport.Domain.Entities.Payment
{
    public class PaymentTransaction : BaseEntity
    {
        public int PaymentId { get; set; }
        public Payment Payment { get; set; }

        // Ödeme Detayları
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string? Description { get; set; }
        public int? StudentId { get; set; }
        public SchoolTransport.Domain.Entities.Student.Student Student { get; set; }

    }

}
