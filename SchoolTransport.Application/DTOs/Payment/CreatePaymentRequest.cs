using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTransport.Application.DTOs.Payment
{
    public class CreatePaymentRequest
    {
        public int StudentId { get; set; }
        public decimal Pay { get; set; }
        public string Notes { get; set; }
        public int periodNumber { get; set; }

    }
}
