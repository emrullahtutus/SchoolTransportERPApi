using SchoolTransport.Application.Exceptions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTransport.Application.Exceptions.BadRequest
{
    public class PaymentTransactionBadRequestException : BaseBadRequestException
    {
        public PaymentTransactionBadRequestException() : base("Ödeme işlem verileri eksik veya hatalı.") { }
        public PaymentTransactionBadRequestException(string message) : base(message) { }
    }
}
