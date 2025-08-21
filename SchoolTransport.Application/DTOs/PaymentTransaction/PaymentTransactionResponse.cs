using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTransport.Application.DTOs.PaymentTransaction
{
    public class PaymentTransactionResponse
    {
       
        public decimal Amount { get; set; }
        public string PaymentDate { get; set; }
        public string Description { get; set; }
    }
}
