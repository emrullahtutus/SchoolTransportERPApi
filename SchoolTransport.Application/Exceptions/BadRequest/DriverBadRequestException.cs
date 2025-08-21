using SchoolTransport.Application.Exceptions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTransport.Application.Exceptions.BadRequest
{
    public class DriverBadRequestException : BaseBadRequestException
    {
        public DriverBadRequestException() : base("Sürücü verileri eksik veya hatalı.") { }
        public DriverBadRequestException(string message) : base(message) { }
    }
}
