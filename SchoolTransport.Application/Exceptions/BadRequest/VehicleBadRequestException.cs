using SchoolTransport.Application.Exceptions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTransport.Application.Exceptions.BadRequest
{
    public class VehicleBadRequestException : BaseBadRequestException
    {
        public VehicleBadRequestException() : base("Araç verileri eksik veya hatalı.") { }
        public VehicleBadRequestException(string message) : base(message) { }
    }
}
