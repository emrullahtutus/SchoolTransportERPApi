using SchoolTransport.Application.Exceptions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTransport.Application.Exceptions.BadRequest
{
    public class StudentBadRequestException : BaseBadRequestException
    {
        public StudentBadRequestException() : base("Öğrenci verileri eksik veya hatalı.") { }
        public StudentBadRequestException(string message) : base(message) { }
    }
}
