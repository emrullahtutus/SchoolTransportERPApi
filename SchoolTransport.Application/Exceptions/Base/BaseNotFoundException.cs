using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTransport.Application.Exceptions.Base
{
    public class BaseNotFoundException : Exception
    {
        public BaseNotFoundException()
        {
        }

        public BaseNotFoundException(string message) : base(message)
        {
        }

   
    }
}
