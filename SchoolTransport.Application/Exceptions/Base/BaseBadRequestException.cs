using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTransport.Application.Exceptions.Base
{
    public class BaseBadRequestException : Exception
    {
        public BaseBadRequestException()
        {
        }

        public BaseBadRequestException(string message) : base(message)
        {
        }

        
    }
}
