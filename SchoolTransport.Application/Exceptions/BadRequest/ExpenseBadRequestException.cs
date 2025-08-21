using SchoolTransport.Application.Exceptions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTransport.Application.Exceptions.BadRequest
{

    public class ExpenseBadRequestException : BaseBadRequestException
    {
        public ExpenseBadRequestException() : base("Masraf verileri eksik veya hatalı.") { }
        public ExpenseBadRequestException(string message) : base(message) { }
    }
}
