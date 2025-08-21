using SchoolTransport.Application.Exceptions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTransport.Application.Exceptions.BadRequest
{
    public class PaymentBadRequestException : BaseBadRequestException
    {
        public PaymentBadRequestException() : base("Ödeme verileri eksik veya hatalı.") { }
        public PaymentBadRequestException(string message) : base(message) { }
    }
}
