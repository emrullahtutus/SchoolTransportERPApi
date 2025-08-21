using SchoolTransport.Application.Exceptions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTransport.Application.Exceptions.NotFound
{

    public class PaymentNotFoundException : BaseNotFoundException
    {
        public PaymentNotFoundException(string message) : base(message) { }
        public PaymentNotFoundException(int id) : base($"Id'si {id} olan ödeme bulunamadı.") { }
    }
}
