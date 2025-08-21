using SchoolTransport.Application.Exceptions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTransport.Application.Exceptions.NotFound
{
    public class DriverNotFoundException : BaseNotFoundException
    {
        public DriverNotFoundException(Guid driverId) : base($"Id: {driverId} olan sürücü bulunamadı.") { }
        public DriverNotFoundException(string message) : base(message) { }
    }
}
