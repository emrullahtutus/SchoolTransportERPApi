using SchoolTransport.Application.Exceptions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTransport.Application.Exceptions.BadRequest
{
    public class SchoolBadRequestException : BaseBadRequestException
    {
        public SchoolBadRequestException() : base("Okul verileri eksik veya hatalı.") { }
        public SchoolBadRequestException(string message) : base(message) { }
    }
}
