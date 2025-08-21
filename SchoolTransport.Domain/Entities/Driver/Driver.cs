using SchoolTransport.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTransport.Domain.Entities.Driver
{
    public class Driver : BaseEntity
    {
        public string Name { get; set; } // Şoför adı - Required

        // Vehicle ile 1-1 ilişki (Her şoför sadece bir araç kullanabilir)
        public Vehicle.Vehicle? Vehicle { get; set; }
    }
}
