using SchoolTransport.Application.Exceptions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTransport.Application.Exceptions.NotFound
{
    public class PaymentTransactionNotFoundException : BaseNotFoundException
    {
        public PaymentTransactionNotFoundException(string message) : base(message) { }
        public PaymentTransactionNotFoundException(int id) : base($"Id'si {id} olan ödeme işlemi bulunamadı.") { }
    }
}
