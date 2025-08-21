using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SchoolTransport.Application.Exceptions.Base
{

    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public DateTime DateTime { get; set; }
        public ErrorDetails()
        {
            DateTime = DateTime.UtcNow;
        }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }

    }
}
