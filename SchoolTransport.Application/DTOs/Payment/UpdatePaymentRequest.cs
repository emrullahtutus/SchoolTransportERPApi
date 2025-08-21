using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTransport.Application.DTOs.Payment
{
    public class UpdatePaymentRequest
    {
        public int Id { get; set; }
        public decimal TotalFee { get; set; }
        public decimal MonthlyFee { get; set; }
        public string Notes { get; set; }
      
    }
}
