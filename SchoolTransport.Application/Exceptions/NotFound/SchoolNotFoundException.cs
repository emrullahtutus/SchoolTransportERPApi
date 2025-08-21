using SchoolTransport.Application.Exceptions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTransport.Application.Exceptions.NotFound
{
    public class SchoolNotFoundException : BaseNotFoundException
    {
        public SchoolNotFoundException(Guid schoolId) : base($"Id: {schoolId} olan okul bulunamadı.") { }
        public SchoolNotFoundException(string message) : base(message) { }
    }
}
