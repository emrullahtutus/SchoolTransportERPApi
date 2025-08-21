using SchoolTransport.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTransport.Domain.Entities.Activity
{
    public class Activity : BaseEntity
    {
        public string Name { get; set; }
        public DateTime DateTime { get; set; }
        public string? Description { get; set; }
        public string? Notes { get; set; }
        // bi etkinlik sadece 1 araca ait olabilir
        public  int VehicleId { get; set; }
        public Vehicle.Vehicle Vehicle { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
