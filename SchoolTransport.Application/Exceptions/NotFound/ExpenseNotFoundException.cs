using SchoolTransport.Application.Exceptions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTransport.Application.Exceptions.NotFound
{
    public class ExpenseNotFoundException : BaseNotFoundException
    {
        public ExpenseNotFoundException(int expenseId) : base($"Id: {expenseId} olan masraf bulunamadı.") { }
        public ExpenseNotFoundException(string message) : base(message) { }
    }
}
