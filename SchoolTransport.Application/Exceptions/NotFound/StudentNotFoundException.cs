using SchoolTransport.Application.Exceptions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTransport.Application.Exceptions.NotFound
{
    public class StudentNotFoundException : BaseNotFoundException
    {
        public StudentNotFoundException(Guid studentId) : base($"Id: {studentId} olan öğrenci bulunamadı.") { }
        public StudentNotFoundException(string message) : base(message) { }
    }
}
